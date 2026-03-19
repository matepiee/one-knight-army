using UnityEngine;
using TMPro;


public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;
    public StatsUI statsUI;
    public TMP_Text healthText;

    public Player_ChangeToGuard guard;
    public Canvas shieldCanvas;
    public CanvasGroup shieldCanvasGroup;

    [Header("Combat Stats")]
    public int damage;
    public float weaponRange;
    public float knockbackForce;
    public float knockbackTime;
    public float stunTime;

    [Header("Movement Stats")]
    public float speed;

    [Header("Health Stats")]
    public int currentHp;
    public int maxHp;

    [Header("Bow Stats")]
    public int ArrowDamage;
    public float shootCooldown = .5f;
    public float shootTimer;

    public void Start()
    {
        shieldCanvas.enabled = false;
        shieldCanvasGroup.alpha = 0;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else { Destroy(gameObject); }
    }

    public void UpdateMaxHealth(int amount)
    {
        maxHp += amount;
        healthText.text = "HP: " + currentHp + "/ " + maxHp;
    }

    public void UpdateHealth(int amount)
    {
        currentHp += amount;

        if (currentHp >= maxHp)
        {
            currentHp = maxHp;
        }

        healthText.text = "HP: " + currentHp + "/ " + maxHp;
    }

    public void UpdateAttackDamage(int amount)
    {
        damage += amount;
    }

    public void UpdateSpeed(float amount)
    {
        speed += amount;
    }


    public void UpdateArrow(int dmg,float cd)
    {
        ArrowDamage+= dmg;
        shootCooldown-= cd;
    }

    public void UnlockGuard()
    {
        guard.enabled = true;
        shieldCanvas.enabled = true;
        shieldCanvasGroup.alpha = 1;
    }


}
