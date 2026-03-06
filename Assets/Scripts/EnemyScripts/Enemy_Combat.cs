using UnityEngine;

public class Enemy_Combat : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask playerLayer;
    public float knockbackForce;
    public float stunTime;
    public float weaponRange;
    public int damage;

    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, weaponRange, playerLayer);

        if (hits.Length > 0)
        {
            GameObject player = hits[0].gameObject;

            if (player.activeInHierarchy)
            {
                Player_Movement movement = player.GetComponent<Player_Movement>();
                if (movement != null)
                {
                    movement.Knockback(transform, knockbackForce, stunTime);
                }

                Player_Health health = player.GetComponent<Player_Health>();
                if (health != null)
                {
                    health.ChangeHealth(-damage);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, weaponRange);
    }
}