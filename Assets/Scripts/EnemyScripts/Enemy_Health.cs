using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public int expReward = 30;

    public delegate void MonsterDefeated(int exp);
    public static event MonsterDefeated OnMonsterDefeated;

    public int currentHp;
    public int maxHp;

    private void Start()
    {
        currentHp = maxHp;
    }

    public void ChangeHealth(int amount)
    {
        currentHp += amount;
        if (currentHp>maxHp)
        {
            currentHp = maxHp;
        }
        else if (currentHp <=0)
        {
            OnMonsterDefeated(expReward);
            Destroy(gameObject);

        }
    }
}
