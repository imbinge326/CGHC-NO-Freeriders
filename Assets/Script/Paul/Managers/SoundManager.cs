using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Background Music")]
    [SerializeField] private AudioClip finalLevelBackgroundMusic;
    [SerializeField] private AudioClip finalBossBackgroundMusic;
    [SerializeField] private float volume = 0.05f;

    [Header("Sound Effects")]
    [SerializeField] private List<AudioClip> soundEffects; // List of all sound effects for easy assignment in the Inspector

    private AudioSource backgroundMusicSource;
    private List<AudioSource> effectSources = new List<AudioSource>(); // Pool of AudioSources for sound effects
    private Dictionary<string, AudioClip> soundEffectDictionary = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;

            backgroundMusicSource = GetComponent<AudioSource>();
            backgroundMusicSource.loop = true; // Loop background music
            backgroundMusicSource.volume = volume;

            foreach (AudioClip clip in soundEffects)
            {
                soundEffectDictionary[clip.name] = clip; // Add each clip to dictionary by name for easy access
            }
        }
    }

    public void StartFinalLevelBackgroundMusic()
    {
        if (finalLevelBackgroundMusic != null)
        {
            backgroundMusicSource.clip = finalLevelBackgroundMusic;
            backgroundMusicSource.Play();
        }
    }

    public void StartFinalBossBGM()
    {
        if (finalBossBackgroundMusic != null)
        {
            backgroundMusicSource.clip = finalBossBackgroundMusic;
            backgroundMusicSource.Play();
        }
    }

    public void PlaySoundEffect(string soundName)
    {
        if (soundEffectDictionary.TryGetValue(soundName, out AudioClip clip))
        {
            AudioSource effectSource = GetAvailableEffectSource();
            effectSource.clip = clip;
            effectSource.Play();
        }
        else
        {
            Debug.LogWarning($"Sound '{soundName}' not found in SoundManager.");
        }
    }

    private AudioSource GetAvailableEffectSource()
    {
        foreach (var source in effectSources)
        {
            if (!source.isPlaying) return source; // Reuse idle source
        }
        // If all sources are in use, create a new one and add to the pool
        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        effectSources.Add(newSource);
        return newSource;
    }
}
