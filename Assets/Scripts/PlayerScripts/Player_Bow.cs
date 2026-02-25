using UnityEngine;

public class Player_Bow : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject arrowPrefab;

    public Player_Movement playerMovement;

    private Vector2 aimDirection = Vector2.right;

    public Animator anim;
    
    void Update()
    {
        StatsManager.Instance.shootTimer -=Time.deltaTime;

        HandleAiming();

        if (Input.GetButtonDown("Shoot") && StatsManager.Instance.shootTimer <=0)
        {
            playerMovement.isShooting= true;
            anim.SetBool("IsShooting", true);
            
        }
        
    }

    public void OnEnable()
    {
        anim.SetLayerWeight(0, 0);
        anim.SetLayerWeight(1, 1);
    }

    public void OnDisable()
    {
        anim.SetLayerWeight(0, 1);
        anim.SetLayerWeight(1, 0);
    }

    public void HandleAiming()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
          
            aimDirection = new Vector2(horizontal, vertical).normalized;
            anim.SetFloat("aimX",aimDirection.x);
            anim.SetFloat("aimY", aimDirection.y);
        }
    }

    public void Shoot()
    { 
        if (StatsManager.Instance.shootTimer <=0)
        {
            Arrow arrow = Instantiate(arrowPrefab, launchPoint.position, Quaternion.identity).GetComponent<Arrow>();
            arrow.direction = aimDirection;
            StatsManager.Instance.shootTimer = StatsManager.Instance.shootCooldown;
        }
        
        anim.SetBool("IsShooting", false);
        playerMovement.isShooting = false;
    }
}
