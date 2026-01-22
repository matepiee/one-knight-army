using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float speed = 5f;
    public int facingDirection = 1;
    public Rigidbody2D rb;
    public Animator anim;

    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // GetAxisRaw a pontosabb irányításért
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal>0&& transform.localScale.x<0|| horizontal < 0 && transform.localScale.x > 0 )
        {
            Flip();
        }

        anim.SetFloat("horizontal",Mathf.Abs(horizontal));
        anim.SetFloat("vertical", Mathf.Abs(vertical));

        // Fontos: Mindig normalizáljuk az irányt, különben átlósan gyorsabb lenne!
        Vector2 movement = new Vector2(horizontal, vertical).normalized;
        rb.linearVelocity = movement * speed;
    }
    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}