using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    public LayerMask enemyLayer;
    public Transform attackPoint;
    public Animator anim;
    public float cooldown = 1;
    public Rigidbody2D rb;
    private float timer;
 

    public void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

    }

    public void Attack()
    {
        if (timer <= 0)
        {
            anim.SetBool("IsAttacking", true);
            rb.constraints = rb.constraints | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

            timer = cooldown;
        }
    }

    public void DealDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, StatsManager.Instance.weaponRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.TryGetComponent(out Enemy_Health health))
            {
                health.ChangeHealth((int)-StatsManager.Instance.damage);

                if (enemy.TryGetComponent(out Enemy_Knockback kb))
                {
                    kb.Knockback(transform, StatsManager.Instance.knockbackForce, StatsManager.Instance.knockbackTime, StatsManager.Instance.stunTime);
                }
            }
        }
    }
    public void FinishAttacking()
    {
        anim.SetBool("IsAttacking", false);
        rb.constraints = rb.constraints & ~RigidbodyConstraints2D.FreezePositionX & ~RigidbodyConstraints2D.FreezePositionY;
    }


    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;

        StatsManager stats = Object.FindFirstObjectByType<StatsManager>();

        if (stats != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, stats.weaponRange);
        }
        else
        {
            Gizmos.DrawWireSphere(attackPoint.position, 1f);
        }
    }

}
