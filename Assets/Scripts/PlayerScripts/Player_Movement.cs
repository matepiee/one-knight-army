using UnityEngine;
using System.Collections;

public class Player_Movement : MonoBehaviour
{
    public int facingDirection = 1;
    public Rigidbody2D rb;
    public Animator anim;

    [Header("Attack Settings")]
    public float attackOffset = 0.8f; // Milyen messze legyen az attackPoint a karaktertõl

    private bool isKnockedBack;
    public bool isShooting;

    public Player_Combat player_Combat;

    private void Update()
    {
        if (Input.GetButtonDown("Slash"))
        {
            player_Combat.Attack();
        }
    }

    void FixedUpdate()
    {
        if (isShooting)
        {
            rb.linearVelocity = Vector2.zero;
        }
        else if (!isKnockedBack)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            // Ez a változó mondja meg, hogy épp zajlik-e az ütés
            bool currentlyAttacking = anim.GetBool("IsAttacking");

            // MOZGÁS: Csak akkor mozoghat és fordulhat, ha NEM üt éppen
            if (!currentlyAttacking)
            {
                // Fordulás tiltása ütés közben
                if (horizontal > 0 && transform.localScale.x < 0 || horizontal < 0 && transform.localScale.x > 0)
                {
                    Flip();
                }

                Vector2 movement = new Vector2(horizontal, vertical).normalized;
                rb.linearVelocity = movement * StatsManager.Instance.speed;

                // ATTACK POINT FRISSÍTÉSE: Csak ha nem ütünk, akkor válthat irányt
                if (movement != Vector2.zero)
                {
                    Vector2 newPos = movement * attackOffset;
                    if (facingDirection == -1)
                    {
                        newPos.x *= -1;
                    }
                    player_Combat.attackPoint.localPosition = newPos;
                }
            }
            else
            {
                // Ha ütünk, ne mozogjunk (opcionális, de stabilabb)
                rb.linearVelocity = Vector2.zero;
            }

            anim.SetFloat("horizontal", Mathf.Abs(horizontal));
            anim.SetFloat("vertical", Mathf.Abs(vertical));
        }
    }
    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public void Knockback(Transform enemy, float force, float stunTime)
    {
        isKnockedBack = true;
        Vector2 direction = (transform.position - enemy.position).normalized;
        rb.linearVelocity = direction * force;
        StartCoroutine(KnockbackCounter(stunTime));
    }


    IEnumerator KnockbackCounter(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        rb.linearVelocity= Vector2.zero;
        isKnockedBack=false;
    }
}