using UnityEngine;

public class LevelMusic : MonoBehaviour
{
    [SerializeField] private AudioClip levelMusic;

    private void Start()
    {
        SoundManager.instance.ChangeMusic(levelMusic);
    }
}