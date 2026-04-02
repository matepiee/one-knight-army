using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 direction = Vector2.right;
    public float lifespan = 2;
    public float speed;

    public LayerMask enemyLayer;
    public LayerMask obstacleLayer;

    public SpriteRenderer sr;
    public Sprite buriedSprite;

    public int ArrowDamage;
    public float knockbackForce;
    public float knockbackTime;
    public float stunTime;
    
    void Start()
    {
        rb.linearVelocity = direction* speed;
        RotateArrow();
        Destroy(gameObject, lifespan);
    }

    private void RotateArrow()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
    }

    public void  OnCollisionEnter2D(Collision2D collision)
    {
        if ((enemyLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            Enemy_Health health = collision.gameObject.GetComponent<Enemy_Health>();
            Enemy_Knockback knockback = collision.gameObject.GetComponent<Enemy_Knockback>();

            if (health != null)
            {
                health.ChangeHealth(-ArrowDamage);
            }

            if (knockback != null)
            {
                knockback.Knockback(transform, knockbackForce, knockbackTime, stunTime);
            }
        } else if ((obstacleLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            AttachToTarget(collision.gameObject.transform);
        }
    }

    public void AttachToTarget(Transform target)
    {
        sr.sprite = buriedSprite;

        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;

        transform.SetParent(target);
    }
}
