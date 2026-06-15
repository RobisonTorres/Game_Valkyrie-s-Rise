using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackCooldown;
    private Animator anim;
    private PlayerMovement playerMovement;
    public Transform attackOrigin;
    public float attackRadius = 1f;
    public LayerMask enemyMask;
    private float cooldownTimer = Mathf.Infinity;

    [SerializeField] AudioClip hitSwordSound;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnDrawGizmos()
    {
        if (attackOrigin != null)
        {
            Gizmos.DrawWireSphere(attackOrigin.position, attackRadius);
        }
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
        {
            Attack();
        }
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        SoundManager.instance.PlaySound(hitSwordSound);
        cooldownTimer = 0;

        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(attackOrigin.position, attackRadius, enemyMask);

        foreach (Collider2D enemyCollider in enemiesInRange)
        {
            if(enemyCollider.tag == "Enemy")
            {
                enemyCollider.GetComponent<Health>().TakeDamage(1);
            }
        }
    }
}