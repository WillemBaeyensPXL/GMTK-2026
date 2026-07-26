using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioSource _pitchSource;
    public MusicTracks MusicTracks;
    public SoundEffects SoundEffects;

    public float MusicVolume
    {
        get => _musicSource.volume;
        set
        {
            _musicSource.volume = value;
        }
    }
    public float SFXVolume
    {
        get => _sfxSource.volume;
        set
        {
            _sfxSource.volume = value;
        }
    }


    public static SoundManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        MusicVolume = .5f;
        SFXVolume = .5f;
    }

    public void PlayMusic(AudioClip clip)
    {
        if (_musicSource.clip != clip)
        {
            _musicSource.clip = clip;
            _musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip != null)
        {
            _sfxSource.volume = SFXVolume;
            _sfxSource.PlayOneShot(clip, volume);
        }
    }

    public void PlaySFXPitch(AudioClip clip, float volume,float pitch)
    {
        if(clip != null)
        {
            _pitchSource.pitch = pitch;
            _pitchSource.PlayOneShot(clip, volume);
        }
    }
}
