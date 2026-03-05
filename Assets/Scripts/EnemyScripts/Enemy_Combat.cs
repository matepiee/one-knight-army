using UnityEngine;
using TMPro;

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
            StatsManager playerStats = hits[0].GetComponent<StatsManager>();

            hits[0].GetComponent<Player_Health>().ChangeHealth(-damage);
            hits[0].GetComponent<Player_Movement>().Knockback(transform, knockbackForce, stunTime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, weaponRange);
    }
}