using UnityEngine;

public class Diamond : MonoBehaviour
{

    [SerializeField] AudioClip take;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SoundManager.instance.PlaySound(take);
            Destroy(gameObject);
            GameManager.score += 1;
        }
    }
}
