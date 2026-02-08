using UnityEngine;

public class Player_Bow : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject arrowPrefab;
    public float shootCooldown = .5f;
    public float shootTimer;


    private Vector2 aimDirection = Vector2.right;

    
    void Update()
    {
        shootTimer -=Time.deltaTime;

        HandleAiming();

        if (Input.GetButtonDown("Shoot") && shootTimer <=0)
        {
            Shoot();
        }
        
    }

    public void HandleAiming()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            aimDirection = new Vector2(horizontal, vertical).normalized;
        }
    }

    public void Shoot()
    {
        Arrow arrow = Instantiate(arrowPrefab, launchPoint.position, Quaternion.identity).GetComponent<Arrow>();
        arrow.direction = aimDirection;
        shootTimer = shootCooldown;
    }
}
