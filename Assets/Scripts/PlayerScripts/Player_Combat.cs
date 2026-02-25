using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    public LayerMask enemyLayer;
    public Transform attackPoint;
    public Animator anim;
    public float cooldown = 1;
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


            timer = cooldown;
        }
    }

    public void DealDamage()
    {
        // 1. Megkeressük az ÖSSZES ellenséget a körön belül
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, StatsManager.Instance.weaponRange, enemyLayer);

        // 2. Végigmegyünk a listán egy ciklussal
        foreach (Collider2D enemy in hitEnemies)
        {
            // Megpróbáljuk elkérni az élet scriptet
            if (enemy.TryGetComponent(out Enemy_Health health))
            {
                // Sebzés kiosztása (AOE - mindenki megkapja!)
                health.ChangeHealth((int)-StatsManager.Instance.damage);

                // Ha van knockback, azt is mindenkin végrehajtjuk
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
    }


    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;

        // Megpróbáljuk megkeresni a jelenetben lévõ StatsManagert
        StatsManager stats = Object.FindFirstObjectByType<StatsManager>();

        if (stats != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, stats.weaponRange);
        }
        else
        {
            Gizmos.DrawWireSphere(attackPoint.position, 1f); // Alapértelmezett
        }
    }

}
