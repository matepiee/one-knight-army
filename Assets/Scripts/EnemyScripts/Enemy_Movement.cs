using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform player;
    private Transform baseTarget;
    private int facingDirection = 1;
    private Animator anim;
    private EnemyState enemyState;
    private float attackCooldownTimer;

    [Header("Options")]
    public float playerDetectRange = 5;
    public float speed = 5f;
    public float attackRange = 1;
    public float attackCooldown = 2;
    public float baseReachDistance = 0.5f;

    [Header("Separation (Soft Collision)")]
    public float separationRadius = 1f; // Milyen távolságból kezdjék el tolni egymást
    public float separationForce = 40f;   // Milyen erővel toljanak
    public LayerMask enemyLayer;

    [Header("References")]
    public Transform detectionPoint;
    public LayerMask playerLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        GameObject baseObj = GameObject.FindGameObjectWithTag("Base");
        if (baseObj != null)
        {
            baseTarget = baseObj.transform;
        }

        ChangeState(EnemyState.Idle);
    }

    void Update()
    {
        if (enemyState != EnemyState.Knockback)
        {
            CheckForPlayer();

            if (attackCooldownTimer > 0)
            {
                attackCooldownTimer -= Time.deltaTime;
            }

            if (enemyState == EnemyState.Chasing)
            {
                Move(player);
            }
            else if (enemyState == EnemyState.Idle)
            {
                if (baseTarget != null)
                {
                    Move(baseTarget);
                    CheckBaseArrival();
                }
                else
                {
                    rb.linearVelocity = Vector2.zero;
                }
            }
            else if (enemyState == EnemyState.Attacking)
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
    }

    void FixedUpdate()
    {
        ApplySeparationForce();
    }

    void ApplySeparationForce()
    {
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, separationRadius, enemyLayer);

        foreach (Collider2D enemy in nearbyEnemies)
        {
            if (enemy.gameObject != gameObject)
            {
                Vector2 pushDirection = (transform.position - enemy.transform.position).normalized;

                float distance = Vector2.Distance(transform.position, enemy.transform.position);
                float forceStrength = (1f - (distance / separationRadius)) * separationForce;

                rb.AddForce(pushDirection * forceStrength);
            }
        }
    }
    void Move(Transform target)
    {
        if (target == null) return;

        if (target.position.x > transform.position.x && facingDirection == -1 || target.position.x < transform.position.x && facingDirection == 1)
        {
            Flip();
        }

        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }

    void CheckBaseArrival()
    {
        if (Vector2.Distance(transform.position, baseTarget.position) <= baseReachDistance)
        {
            AttackBase();
        }
    }

    void AttackBase()
    {
        Base_Health baseHealth = baseTarget.GetComponent<Base_Health>();

        if (baseHealth != null)
        {
            baseHealth.TakeDamage(1);
        }

        Destroy(gameObject);
    }

    private void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, playerDetectRange, playerLayer);

        if (hits.Length > 0)
        {
            player = hits[0].transform;

            float distance = Vector2.Distance(transform.position, player.position);

            if (distance <= attackRange && attackCooldownTimer <= 0)
            {
                attackCooldownTimer = attackCooldown;
                ChangeState(EnemyState.Attacking);
            }
            else if (distance > attackRange && enemyState != EnemyState.Attacking)
            {
                ChangeState(EnemyState.Chasing);
            }
        }
        else
        {
            if (enemyState != EnemyState.Idle)
            {
                ChangeState(EnemyState.Idle);
            }
        }
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public void ChangeState(EnemyState newState)
    {
        if (enemyState == newState) return;

        anim.SetBool("IsIdle", false);
        anim.SetBool("IsChasing", false);
        anim.SetBool("IsAttacking", false);

        enemyState = newState;

        if (enemyState == EnemyState.Idle) anim.SetBool("IsIdle", true);
        else if (enemyState == EnemyState.Chasing) anim.SetBool("IsChasing", true);
        else if (enemyState == EnemyState.Attacking) anim.SetBool("IsAttacking", true);
    }

    public void ResetToIdle()
    {
        ChangeState(EnemyState.Idle);
    }

    public void ResetToChasing()
    {
        ChangeState(EnemyState.Chasing);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectionPoint.position, playerDetectRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, separationRadius);
    }
}

public enum EnemyState
{
    Idle,
    Chasing,
    Attacking,
    Knockback
}