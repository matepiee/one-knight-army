using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;

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

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else { Destroy(gameObject); }
    }


}
