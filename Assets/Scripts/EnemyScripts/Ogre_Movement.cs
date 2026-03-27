using UnityEngine;
using System.Collections;

public class Ogre_Movement : Enemy_Movement // Az Enemy_Movement-bõl származik le
{
    [Header("Ogre Dash Settings")]
    public float dashDistance = 6f;
    public float dashSpeed = 18f;
    public float dashDuration = 0.4f;
    public float dashCooldown = 4f;
    private float dashCooldownTimer;
    private bool isDashing = false;

    // Felülírjuk az alap keresést az Ogre saját logikájával
    protected override void CheckForPlayer()
    {
        if (isDashing) return;

        if (dashCooldownTimer > 0) dashCooldownTimer -= Time.deltaTime;

        // Meghívjuk az alap keresést (beállítja a 'player' változót és a távolságot)
        base.CheckForPlayer();

        // Ha az alap script nem talált játékost, mi sem tudunk mit tenni
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null) return;

        float distance = Vector2.Distance(transform.position, playerObj.transform.position);

        // Ogre-specifikus Dash logika
        if (distance > attackRange && distance <= dashDistance && dashCooldownTimer <= 0)
        {
            StartCoroutine(OgreDashRoutine(playerObj.transform));
        }
    }

    IEnumerator OgreDashRoutine(Transform target)
    {
        isDashing = true;
        dashCooldownTimer = dashCooldown;

        ChangeState(EnemyState.Dash); // Az enumot bõvítsd ki a Dash-el!

        Vector2 dashDir = (target.position - transform.position).normalized;

        float startTime = Time.time;
        while (Time.time < startTime + dashDuration)
        {
            GetComponent<Rigidbody2D>().linearVelocity = dashDir * dashSpeed;
            yield return null;
        }

        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        isDashing = false;
        ChangeState(EnemyState.Chasing);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Csak akkor foglalkozunk az ütközéssel, ha a Player aktív (él)
        if (isDashing && collision.CompareTag("Player") && collision.gameObject.activeInHierarchy)
        {
            Enemy_Combat combat = GetComponent<Enemy_Combat>();
            if (combat != null)
            {
                collision.GetComponent<Player_Health>()?.ChangeHealth(-combat.damage);

                // Csak akkor hívjuk a Knockback-et, ha a játékos még aktív a sebzés után is
                if (collision.gameObject.activeInHierarchy)
                {
                    collision.GetComponent<Player_Movement>()?.Knockback(transform, combat.knockbackForce, combat.stunTime);
                }
            }
        }
    }
}