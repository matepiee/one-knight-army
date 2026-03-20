using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;
    public StatsUI statsUI;
    public TMP_Text healthText;

    [Header("Rage")]
    public Player_Rage rage;
    public CanvasGroup rageUnlock;
    public Button rageButton;

    [Header("Guard")]
    public Player_ChangeToGuard guard;
    public Canvas shieldCanvas;
    public CanvasGroup shieldCanvasGroup;

    [Header("Heal")]
    public Button healButton;
    public Player_Heal healing;
    public CanvasGroup healUnlock;

    [Header("Bow")]
    public Player_ChangeEquipment bow;
    public Button arrowUpgrade;
    public CanvasGroup locked;

    [Header("Combat Stats")]
    public int damage;
    public float weaponRange;
    public float knockbackForce;
    public float knockbackTime;
    public float stunTime;
    public int rageProgress;

    [Header("Movement Stats")]
    public float speed;

    [Header("Health Stats")]
    public int currentHp;
    public int maxHp;
    public int heal = 0;

    [Header("Bow Stats")]
    public int ArrowDamage;
    public float shootCooldown = .5f;
    public float shootTimer;

    public void Start()
    {
        shieldCanvas.enabled = false;
        shieldCanvasGroup.alpha = 0;
        arrowUpgrade.interactable = false;
        rage.enabled = false;
        rageButton.interactable = false;
        healing.enabled = false;
        healButton.interactable = false;
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

    public void Heal(int amount)
    {
        heal += amount;
        healing.enabled = true;
        healButton.interactable = true;
        healUnlock.alpha = 0;
    }

    public void UnlockArchery()
    {
        bow.enabled = true;
        arrowUpgrade.interactable = true;
        locked.alpha = 0;
    }

    public void UnlockRage()
    {
        rageProgress+=1;
        if (rageProgress==5)
        {
            rage.enabled = true;
            rageButton.interactable = true;
            rageUnlock.alpha = 0;
        }
        
        Debug.Log(rageProgress);
    }
}
