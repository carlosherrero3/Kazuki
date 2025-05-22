using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class AudioSystem : MonoBehaviour
{
    [System.Serializable]
    public class AudioTrack
    {
        public string name;
        public AudioClip clip;
        public string[] allowedScenes;
        [Range(0f, 1f)] public float volume = 1f;
        public bool loop = true;
        [HideInInspector] public float currentTime = 0f;
    }

    [System.Serializable]
    public class SoundEffect
    {
        public string name;
        public AudioClip clip;
        public string[] allowedScenes;
        [Range(0f, 1f)] public float volume = 1f;
    }

    [System.Serializable]
    public class DialogueTrack
    {
        public string name;
        public AudioClip clip;
        public string[] allowedScenes;
        [Range(0f, 1f)] public float volume = 1f;
    }

    [Header("Configuración")]
    public List<AudioTrack> musicTracks = new List<AudioTrack>();
    public List<SoundEffect> soundEffects = new List<SoundEffect>();
    public List<DialogueTrack> dialogueTracks = new List<DialogueTrack>();
    public float fadeDuration = 1f;

    private AudioSource musicSource;
    private AudioSource dialogueSource;
    private Dictionary<string, AudioSource> soundEffectSources = new Dictionary<string, AudioSource>();
    private static AudioSystem instance;
    private AudioTrack currentMusicTrack;
    private DialogueTrack currentDialogueTrack;

    private void Awake()
    {
        // Singleton pattern
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        // Configurar fuentes de audio
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.playOnAwake = false;
        musicSource.volume = 0f;

        dialogueSource = gameObject.AddComponent<AudioSource>();
        dialogueSource.loop = false;
        dialogueSource.playOnAwake = false;
        dialogueSource.volume = 0f;

        // Crear AudioSources para efectos de sonido
        foreach (var effect in soundEffects)
        {
            var source = gameObject.AddComponent<AudioSource>();
            source.clip = effect.clip;
            source.volume = effect.volume;
            source.loop = false;
            source.playOnAwake = false;
            soundEffectSources[effect.name] = source;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Manejar música
        HandleMusicForScene(scene.name);

        // Manejar diálogos
        HandleDialoguesForScene(scene.name);
    }

    #region Música
    private void HandleMusicForScene(string sceneName)
    {
        // Buscar pistas válidas para esta escena
        List<AudioTrack> validTracks = musicTracks.FindAll(t => IsSceneAllowed(sceneName, t.allowedScenes));

        if (validTracks.Count == 0)
        {
            StopMusic();
            return;
        }

        // Si la pista actual es válida, continuar
        if (currentMusicTrack != null && validTracks.Contains(currentMusicTrack))
        {
            return;
        }

        // Elegir nueva pista (aquí puedes implementar lógica de selección)
        PlayMusic(validTracks[0].name);
    }

    public void PlayMusic(string trackName)
    {
        AudioTrack track = musicTracks.Find(t => t.name == trackName);
        if (track == null)
        {
            Debug.LogWarning($"Music track '{trackName}' not found");
            return;
        }

        // Guardar tiempo de la pista actual
        if (currentMusicTrack != null)
        {
            currentMusicTrack.currentTime = musicSource.time;
        }

        currentMusicTrack = track;
        StartCoroutine(CrossFadeMusic(track.clip, track.volume, track.currentTime, track.loop));
    }

    private IEnumerator CrossFadeMusic(AudioClip newClip, float targetVolume, float startTime, bool loop)
    {
        musicSource.clip = newClip;
        musicSource.time = startTime;
        musicSource.loop = loop;
        musicSource.Play();

        float time = 0f;
        float startVolume = musicSource.volume;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, targetVolume, time / fadeDuration);
            yield return null;
        }

        musicSource.volume = targetVolume;
    }

    public void StopMusic()
    {
        if (currentMusicTrack != null)
        {
            currentMusicTrack.currentTime = musicSource.time;
            StartCoroutine(FadeOutMusic());
        }
    }

    private IEnumerator FadeOutMusic()
    {
        float time = 0f;
        float startVolume = musicSource.volume;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, 0f, time / fadeDuration);
            yield return null;
        }

        musicSource.Stop();
        currentMusicTrack = null;
    }
    #endregion

    #region Efectos de Sonido
    public void PlaySoundEffect(string effectName)
    {
        SoundEffect effect = soundEffects.Find(e => e.name == effectName);
        if (effect == null)
        {
            Debug.LogWarning($"Sound effect '{effectName}' not found");
            return;
        }

        if (soundEffectSources.TryGetValue(effectName, out AudioSource source))
        {
            if (!source.isPlaying)
            {
                source.volume = effect.volume;
                source.Play();
            }
        }
    }

    public void StopSoundEffect(string effectName)
    {
        if (soundEffectSources.TryGetValue(effectName, out AudioSource source))
        {
            if (source.isPlaying)
            {
                source.Stop();
            }
        }
    }
    #endregion

    #region Diálogos
    private void HandleDialoguesForScene(string sceneName)
    {
        // Buscar diálogos válidos para esta escena
        List<DialogueTrack> validDialogues = dialogueTracks.FindAll(d => IsSceneAllowed(sceneName, d.allowedScenes));

        if (validDialogues.Count == 0)
        {
            StopDialogue();
            return;
        }

        // Si el diálogo actual es válido, continuar
        if (currentDialogueTrack != null && validDialogues.Contains(currentDialogueTrack))
        {
            return;
        }

        // Elegir nuevo diálogo (aquí puedes implementar lógica de selección)
        PlayDialogue(validDialogues[0].name);
    }

    public void PlayDialogue(string dialogueName)
    {
        DialogueTrack dialogue = dialogueTracks.Find(d => d.name == dialogueName);
        if (dialogue == null)
        {
            Debug.LogWarning($"Dialogue '{dialogueName}' not found");
            return;
        }

        currentDialogueTrack = dialogue;
        StartCoroutine(FadeInDialogue(dialogue.clip, dialogue.volume));
    }

    private IEnumerator FadeInDialogue(AudioClip newClip, float targetVolume)
    {
        dialogueSource.clip = newClip;
        dialogueSource.Play();

        float time = 0f;
        float startVolume = dialogueSource.volume;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            dialogueSource.volume = Mathf.Lerp(startVolume, targetVolume, time / fadeDuration);
            yield return null;
        }

        dialogueSource.volume = targetVolume;
    }

    public void StopDialogue()
    {
        if (currentDialogueTrack != null)
        {
            StartCoroutine(FadeOutDialogue());
        }
    }

    private IEnumerator FadeOutDialogue()
    {
        float time = 0f;
        float startVolume = dialogueSource.volume;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            dialogueSource.volume = Mathf.Lerp(startVolume, 0f, time / fadeDuration);
            yield return null;
        }

        dialogueSource.Stop();
        currentDialogueTrack = null;
    }
    #endregion

    private bool IsSceneAllowed(string sceneName, string[] allowedScenes)
    {
        foreach (string name in allowedScenes)
        {
            if (sceneName == name) return true;
        }
        return false;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Singleton access
    public static AudioSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioSystem>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "AudioSystem";
                    instance = obj.AddComponent<AudioSystem>();
                }
            }
            return instance;
        }
    }
}