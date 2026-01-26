using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
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
            Destroy(gameObject);
        }
    }
}
