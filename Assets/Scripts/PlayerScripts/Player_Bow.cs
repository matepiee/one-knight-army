using UnityEngine;

public class Player_Bow : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject arrowPrefab;

    public Player_Movement playerMovement;

    private Vector2 aimDirection = Vector2.right;

    public Animator anim;

    // opcionális: ha szeretnéd, hogy az utolsó irány maradjon, ha nincs input
    private Vector2 lastAimDirection = Vector2.right;

    private float timeSinceEnabled;           // a korábbi probléma miatt

    void OnEnable()
    {
        anim.SetLayerWeight(0, 0);
        anim.SetLayerWeight(1, 1);
        timeSinceEnabled = Time.time;
        StatsManager.Instance.shootTimer = StatsManager.Instance.shootCooldown;
    }

    void Update()
    {
        StatsManager.Instance.shootTimer -= Time.deltaTime;

        HandleAiming();

        bool canShootNow = (Time.time - timeSinceEnabled > 0.18f); // kb. 3-4 frame grace period

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

        // Ha van bemenet ? frissítjük az irányt
        if (currentInput.sqrMagnitude > 0.01f)   // kis deadzone, hogy ne remegjen
        {
            aimDirection = currentInput.normalized;
            lastAimDirection = aimDirection;      // mentjük az utolsó érvényes irányt
        }
        // Ha NINCS bemenet ? marad az utolsó ismert irány
        else
        {
            aimDirection = lastAimDirection;
        }

        // Animator paraméterek frissítése (legtöbbször float-ok kellenek blend tree-hez)
        anim.SetFloat("aimX", aimDirection.x);
        anim.SetFloat("aimY", aimDirection.y);

        // Opcionális: ha van "aimMagnitude" paramétered a blend tree-ben
        // anim.SetFloat("aimMagnitude", aimDirection.magnitude);

        // Extra tipp: ha 8 irányú snap-et szeretnél (csak derékszögek)
        // aimDirection = SnapToEightDirections(aimDirection);
    }

    // Opcionális segédfüggvény – ha pontosan 8 irányba akarod snap-elni a célzást
    private Vector2 SnapToEightDirections(Vector2 dir)
    {
        if (dir.sqrMagnitude < 0.01f) return dir;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle = Mathf.Round(angle / 45f) * 45f;           // 45 fokos lépések
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
    }

    // Ezt az animáció event hívja meg (animation event a shoot animáció végén)
    public void Shoot()
    {
        if (StatsManager.Instance.shootTimer > 0) return;

        Arrow arrow = Instantiate(arrowPrefab, launchPoint.position, Quaternion.identity)
            .GetComponent<Arrow>();

        arrow.direction = aimDirection;
        arrow.speed = 12f;           // vagy StatsManagerbõl vedd, ha van
        arrow.damage = StatsManager.Instance.damage;  // ha szeretnéd egységesíteni

        StatsManager.Instance.shootTimer = StatsManager.Instance.shootCooldown;

        anim.SetBool("IsShooting", false);
        playerMovement.isShooting = false;
    }
}