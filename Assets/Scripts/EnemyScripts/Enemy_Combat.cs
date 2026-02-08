using UnityEngine;
using TMPro;

public class Enemy_Combat : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask playerLayer;
    public float knockbackForce;
    public float stunTime;
    public float weaponRange;
    public int damage = 10;

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player_Health>().ChangeHealth(-damage);
        }

    }*/

    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, playerLayer);

        if (hits.Length > 0)
        {
            // 1. Megkeressük a statisztikákat a játékoson
            StatsManager playerStats = hits[0].GetComponent<StatsManager>();

            // 2. Kiszámoljuk a sebzést a konkrét páncél értékkel
            int currentArmor = (playerStats != null) ? playerStats.armor : 0;
            int finalDamage = Mathf.Max(1, 10 - currentArmor);

            // 3. Sebzés és ellökés
            hits[0].GetComponent<Player_Health>().ChangeHealth(-finalDamage);
            hits[0].GetComponent<Player_Movement>().Knockback(transform, knockbackForce, stunTime);
        }
    }
}