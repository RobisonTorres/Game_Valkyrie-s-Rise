using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [SerializeField] AudioClip hurt;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip life;

    private PlayerMovement playerMovement;
    private UIManager uiManager;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        uiManager = Object.FindAnyObjectByType<UIManager>();
        playerMovement = GetComponent<PlayerMovement>();

        if (anim == null)
        {
            anim = GetComponentInChildren<Animator>();
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if (anim == null)
        {
            Debug.LogWarning($"[Health Script] O objeto chamado '{gameObject.name}' tomou dano, mas ele N�O possui um componente Animator anexado!");
            ChecarMorteSemAnim();
            return;
        }

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            SoundManager.instance.PlaySound(hurt);
        }
        else
            {
                if (!dead)
                {
                    anim.SetTrigger("death");
                    if (SoundManager.instance != null)
                    {
                        SoundManager.instance.PlaySound(death);
                    }
                    DesativarComponentes();
                    if (playerMovement != null)
                    {
                        GameManager.score = 0;
                        if (uiManager == null)
                        {
                            uiManager = FindAnyObjectByType<UIManager>();
                        }
                        if (uiManager != null)
                        {
                            uiManager.GameOver();
                        }
                    }
                }
            }
    }

    private void DesativarComponentes()
    {
        if (GetComponent<PlayerMovement>() != null)
            GetComponent<PlayerMovement>().enabled = false;

        if (GetComponentInParent<EnemyPatrol>() != null)
            GetComponentInParent<EnemyPatrol>().enabled = false;

        if (GetComponent<EnemyAttack>() != null)
            GetComponent<EnemyAttack>().enabled = false;

        dead = true;
    }

    private void ChecarMorteSemAnim()
    {
        if (currentHealth <= 0 && !dead)
        {
            DesativarComponentes();
        }
    }

    public void AddHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, startingHealth);
        SoundManager.instance.PlaySound(life);
    }

    private void Deactivete()
    {
        gameObject.SetActive(false);
    }
}   