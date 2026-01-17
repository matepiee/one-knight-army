using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;

    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // GetAxisRaw a pontosabb irányításért
        float vertical = Input.GetAxisRaw("Vertical");

        // Fontos: Mindig normalizáljuk az irányt, különben átlósan gyorsabb lenne!
        Vector2 movement = new Vector2(horizontal, vertical).normalized;
        rb.linearVelocity = movement * speed;
    }
}