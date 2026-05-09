using UnityEngine;

public class ContinueMusic : MonoBehaviour
{
    public static ContinueMusic Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;

    [Header("Default Audio")]
    [SerializeField] private AudioClip defaultMusic;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayMusic(defaultMusic);
    }

    public void PlayMusic(AudioClip music)
    {
        if (music == null) return;

        if (musicSource.clip == music && musicSource.isPlaying)
            return;

        musicSource.clip = music;
        musicSource.loop = true;
        musicSource.Play();
    }
}