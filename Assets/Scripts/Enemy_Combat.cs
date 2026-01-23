using UnityEngine;

public class Enemy_Combat : MonoBehaviour
{
    public int damage = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player_Health>().ChangeHealth(-damage);
        }

    }

}