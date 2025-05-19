using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public float fadeDuration = 2f;

    private static MusicManager instance;
    [SerializeField] private string[] allowedScenes;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource.volume = 0f;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (IsSceneAllowed(scene.name))
        {
            if (!audioSource.isPlaying)
                audioSource.Play();

            StopAllCoroutines();
            StartCoroutine(FadeVolume(1f));
        }
        else
        {
            if (audioSource.isPlaying)
            {
                StopAllCoroutines();
                StartCoroutine(FadeOutAndStop());
            }
        }
    }

    private bool IsSceneAllowed(string sceneName)
    {
        foreach (string name in allowedScenes)
        {
            if (sceneName == name) return true;
        }
        return false;
    }

    private System.Collections.IEnumerator FadeVolume(float targetVolume)
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

    private System.Collections.IEnumerator FadeOutAndStop()
    {
        yield return FadeVolume(0f);
        audioSource.Stop();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
