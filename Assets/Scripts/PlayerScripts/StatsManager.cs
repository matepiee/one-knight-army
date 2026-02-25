using UnityEngine;
using TMPro;


public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;
    public StatsUI statsUI;
    public TMP_Text healthText;

    [Header("Combat Stats")]
    public int damage;
    public float weaponRange;
    public float knockbackForce;
    public float knockbackTime;
    public float stunTime;
    public int armor;
    public int goldgain;

    [Header("Movement Stats")]
    public float speed;

    [Header("Health Stats")]
    public float currentHp;
    public float maxHp;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else { Destroy(gameObject); }
    }

    public void UpdateMaxHealth(float amount)
    {
        maxHp += amount;
        healthText.text = "HP: " + currentHp + "/ " + maxHp;
    }

    public void UpdateHealth(float amount)
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

    public void UpdateMaxSpeed(float amount)
    {
        speed += amount;
    }

    public void UpdateArmor(int amount)
    {
        armor += amount;
    }
    public void UpdateGoldGain(int amount)
    {
        goldgain += amount;
    }
        

}
