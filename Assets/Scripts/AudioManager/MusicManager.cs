using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [Header("Audio Components")]
    public AudioSource musicSource;
    public AudioMixerGroup musicMixerGroup;

    [Header("Zeneszámok")]
    public AudioClip mainMenuMusic;
    public AudioClip preparationMusic;
    public AudioClip gameMusic;
    public AudioClip pauseMusic;

    [Header("Áttűnés Beállításai")]
    [Tooltip("Hány másodperc alatt halkuljon el/fel a zene?")]
    public float fadeDuration = 0.5f;

    private AudioClip previousMusic;
    private float previousTime;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
        }

        musicSource.loop = true;
        musicSource.outputAudioMixerGroup = musicMixerGroup;
    }

    public void PlayMainMenuMusic() => StartFade(mainMenuMusic);
    public void PlayPreparationMusic() => StartFade(preparationMusic);
    public void PlayGameMusic() => StartFade(gameMusic);

    public void PlayPauseMusic()
    {
        if (musicSource.clip == pauseMusic) return;
        previousMusic = musicSource.clip;
        previousTime = musicSource.time;
        StartFade(pauseMusic, 0f);
    }

    public void ResumePreviousMusic()
    {
        if (previousMusic != null)
        {
            StartFade(previousMusic, previousTime);
        }
        else
        {
            PlayPreparationMusic();
        }
    }
    private void StartFade(AudioClip newTrack, float startTime = 0f)
    {
        if (newTrack == null || musicSource.clip == newTrack) return;
        StopAllCoroutines();
        StartCoroutine(FadeRoutine(newTrack, startTime));
    }

    private IEnumerator FadeRoutine(AudioClip newTrack, float startTime)
    {
        while (musicSource.volume > 0)
        {
            musicSource.volume -= 1f * Time.unscaledDeltaTime / fadeDuration;
            yield return null;
        }
        musicSource.volume = 0;
        musicSource.clip = newTrack;
        musicSource.time = startTime;
        musicSource.Play();
        while (musicSource.volume < 1f)
        {
            musicSource.volume += 1f * Time.unscaledDeltaTime / fadeDuration;
            yield return null;
        }
        musicSource.volume = 1f;
    }
}