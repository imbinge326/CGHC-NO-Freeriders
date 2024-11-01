using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;
    [Header ("-----Audio Source-----")]
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource SFX;

    [Header ("-----Audio Clip-----")]
    public AudioClip backgroundMusic;
    public AudioClip hitSound;
    public AudioClip dieSound;
    public AudioClip swingSword;
    public AudioClip fireFireball;

    void Awake()
    {
        if (audioManager != null && audioManager != this)
        {
            Destroy(gameObject);
            return;
        }
        audioManager = this;

    }

    void Start()
    {
        music.clip = backgroundMusic;
        music.Play();
    }

    public void PlaySFX(AudioClip sound)
    {
        SFX.PlayOneShot(sound);
    }
}
