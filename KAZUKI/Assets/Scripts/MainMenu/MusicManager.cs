using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public float fadeDuration = 2f;

    [System.Serializable]
    public struct SceneMusic
    {
        public string sceneName;
        public AudioClip musicClip;
    }

    [SerializeField] private SceneMusic[] sceneMusicList;

    private static MusicManager instance;
    private Dictionary<string, AudioClip> sceneMusicDict;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        sceneMusicDict = new Dictionary<string, AudioClip>();
        foreach (var item in sceneMusicList)
        {
            if (!sceneMusicDict.ContainsKey(item.sceneName))
                sceneMusicDict[item.sceneName] = item.musicClip;
        }

        audioSource.volume = 0f;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        // In case the first scene is already loaded
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (sceneMusicDict.TryGetValue(scene.name, out AudioClip newClip))
        {
            if (audioSource.clip != newClip)
            {
                StopAllCoroutines();
                StartCoroutine(SwitchTrack(newClip));
            }
            else if (!audioSource.isPlaying)
            {
                audioSource.Play();
                StartCoroutine(FadeVolume(1f));
            }
        }
        else
        {
            // Scene without music
            if (audioSource.isPlaying)
            {
                StopAllCoroutines();
                StartCoroutine(FadeOutAndStop());
            }
        }
    }

    private IEnumerator SwitchTrack(AudioClip newClip)
    {
        yield return FadeVolume(0f);
        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.Play();
        yield return FadeVolume(1f);
    }

    private IEnumerator FadeVolume(float targetVolume)
    {
        float startVolume = audioSource.volume;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, time / fadeDuration);
            yield return null;
        }

        audioSource.volume = targetVolume;
    }

    private IEnumerator FadeOutAndStop()
    {
        yield return FadeVolume(0f);
        audioSource.Stop();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
