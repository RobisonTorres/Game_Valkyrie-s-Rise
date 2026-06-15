using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }

    private AudioSource source;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        source = GetComponentInChildren<AudioSource>();
    }

    public void PlaySound(AudioClip sound)
    {
        if (sound == null || source == null)
            return;

        source.PlayOneShot(sound);
    }

    public void ChangeMusic(AudioClip music)
    {
        if (music == null || source == null)
            return;

        source.Stop();
        source.clip = music;
        source.Play();
    }
}