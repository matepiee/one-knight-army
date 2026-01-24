using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    public float knockbackForce = 20;
    public Transform attackPoint;
    public float weaponRange = 1;
    public LayerMask enemyLayer;
    public int damage = 10;
    public Animator anim;
    public float cooldown = 1;
    private float timer;
    public float knockbackTime = .15f;
    public float stunTime=.3f;

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
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, enemyLayer);

        if (enemies.Length > 0)
        {
            enemies[0].GetComponent<Enemy_Health>().ChangeHealth(-damage);
            enemies[0].GetComponent<Enemy_Knockback>().Knockback(transform, knockbackForce, knockbackTime, stunTime);
        }

    }
    public void FinishAttacking()
    {
        anim.SetBool("IsAttacking", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, weaponRange);
    }
}
