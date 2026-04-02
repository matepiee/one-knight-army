using UnityEngine;

public class Player_Bow : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject arrowPrefab;

    public Player_Movement playerMovement;
    public Player_ChangeToGuard guard;

    private Vector2 aimDirection = Vector2.right;

    public Animator anim;

    private Vector2 lastAimDirection = Vector2.right;

    private float timeSinceEnabled;

    void OnEnable()
    {
        anim.SetLayerWeight(0, 0);
        anim.SetLayerWeight(1, 1);
        guard.canSwitchToGuard = false;
        timeSinceEnabled = Time.time;
        StatsManager.Instance.shootTimer = StatsManager.Instance.shootCooldown;
    }
    void OnDisable()
    {
        guard.canSwitchToGuard = true;
        anim.SetLayerWeight(0, 1);
        anim.SetLayerWeight(1, 0);
    }

    void Update()
    {
        StatsManager.Instance.shootTimer -= Time.deltaTime;

        HandleAiming();

        bool canShootNow = (Time.time - timeSinceEnabled > 0.18f);

        if (canShootNow && Input.GetButtonDown("Shoot") && StatsManager.Instance.shootTimer <= 0)
        {
            playerMovement.isShooting = true;
            anim.SetBool("IsShooting", true);
        }
    }

    public void HandleAiming()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 currentInput = new Vector2(horizontal, vertical);

        if (currentInput.sqrMagnitude > 0.01f) 
        {
            aimDirection = currentInput.normalized;
            lastAimDirection = aimDirection;
        }
        else
        {
            aimDirection = lastAimDirection;
        }

        anim.SetFloat("aimX", aimDirection.x);
        anim.SetFloat("aimY", aimDirection.y);
    }

    private Vector2 SnapToEightDirections(Vector2 dir)
    {
        if (dir.sqrMagnitude < 0.01f) return dir;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle = Mathf.Round(angle / 45f) * 45f; 
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
    }

    public void Shoot()
    {
        if (StatsManager.Instance.shootTimer > 0) return;

        Arrow arrow = Instantiate(arrowPrefab, launchPoint.position, Quaternion.identity)
            .GetComponent<Arrow>();

        arrow.direction = aimDirection;
        arrow.speed = 12f;
        arrow.ArrowDamage = StatsManager.Instance.ArrowDamage;

        StatsManager.Instance.shootTimer = StatsManager.Instance.shootCooldown;

        anim.SetBool("IsShooting", false);
        playerMovement.isShooting = false;
    }
}