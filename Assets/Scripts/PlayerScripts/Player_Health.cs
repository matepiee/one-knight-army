using System.Collections;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
    public TMP_Text healthText;
    public Animator healthTextAnim;
    public float respawnDelay = 3f;

    private Player_Movement moveScript;
    private SpriteRenderer spriteRenderer;
    private Collider2D playerCollider;
    private Rigidbody2D rb;

    private void Awake()
    {
        moveScript = GetComponent<Player_Movement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        UpdateUI();
    }

    public void ChangeHealth(int amount)
    {
        StatsManager.Instance.currentHp += amount;
        StatsManager.Instance.currentHp = Mathf.Clamp(StatsManager.Instance.currentHp, 0, StatsManager.Instance.maxHp);

        healthTextAnim.Play("HP_Text_Animation");
        UpdateUI();

        if (StatsManager.Instance.currentHp <= 0)
        {
            Vector3 spawnPoint = new Vector3(0f, 25f, 0f);
            RespawnManager.Instance.RespawnPlayer(gameObject, 3f, spawnPoint);
        }
    }

    void Die()
    {
        
        StartCoroutine(RespawnRoutine());
    }

    IEnumerator RespawnRoutine()
    {
        // --- HALÁL FÁZIS ---
        moveScript.enabled = false;
        rb.linearVelocity = Vector2.zero;
        spriteRenderer.enabled = false;
        playerCollider.enabled = false;
        transform.position = new Vector3(0f, 28f, 0f);
        yield return new WaitForSeconds(respawnDelay);

        // --- ÚJJÁÉLEDÉS FÁZIS ---
        StatsManager.Instance.currentHp = StatsManager.Instance.maxHp;
        UpdateUI();
        spriteRenderer.enabled = true;
        playerCollider.enabled = true;
        moveScript.enabled = true;
    }

    private void OnEnable()
    {
        if (StatsManager.Instance != null)
        {
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        if (healthText != null && StatsManager.Instance != null)
        {
            healthText.text = "HP:" + StatsManager.Instance.currentHp + "/" + StatsManager.Instance.maxHp;
        }
    }
}