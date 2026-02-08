using UnityEngine;
using TMPro;


public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;
    public TMP_Text healthText;

    [Header("Combat Stats")]
    public int damage;
    public float weaponRange;
    public float knockbackForce;
    public float knockbackTime;
    public float stunTime;
    public int armor;
    public float goldGain;

    [Header("Movement Stats")]
    public float speed;

    [Header("Health Stats")]
    public int currentHp;
    public int maxHp;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else { Destroy(gameObject); }
    }

    public void UpdateMaxHealth(int amount)
    {
        maxHp += amount;
        healthText.text = "HP: " +currentHp +"/ " + maxHp;
    }

    public void UpdateAttackDamage(int amount)
    {
        damage += amount;
    }

    public void UpdateSpeed(float amount)
    {
        speed += amount;
    }

    public void UpdateArmor(int amount)
    {
        armor += amount;
    }

    public void UpdateGoldGain(float amount)
    {
        goldGain += amount;  
    }

}
