using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // just call AudioManager.Instance.PlayHit() and so on to use it
    public static AudioManager Instance;

    [Header("audio sources (add 2 audiosource components to this object one for play on awake)")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    [Header("clips")]
    [SerializeField] private AudioClip _music;
    [SerializeField] private AudioClip _hitSound;         
    [SerializeField] private AudioClip _enemyDeathSound;   
    [SerializeField] private AudioClip _gameOverSound;

    [Header("volume")]
    [SerializeField][Range(0f, 1f)] private float _musicVolume = 0.4f;
    [SerializeField][Range(0f, 1f)] private float _sfxVolume = 0.7f;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _musicSource.clip = _music;
        _musicSource.loop = true;
        _musicSource.volume = _musicVolume;
        _musicSource.Play();
    }

    //plays the clip once so like no overlap
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            _sfxSource.PlayOneShot(clip, _sfxVolume);
        }
    }

    public void PlayHit()
    {
        PlaySFX(_hitSound);
    }

    public void PlayEnemyDeath()
    {
        PlaySFX(_enemyDeathSound);
    }

    // cuts the music and the gameover sound
    public void PlayGameOver()
    {
        _musicSource.Stop();
        PlaySFX(_gameOverSound);
    }
}