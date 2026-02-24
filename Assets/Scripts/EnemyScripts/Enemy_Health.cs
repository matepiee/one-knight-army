using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Health : MonoBehaviour
{
    public int expReward;
    public int goldReward;
    public TMP_Text goldText;

    public delegate void MonsterDefeated(int exp);

    public static event MonsterDefeated OnMonsterDefeated;

    [SerializeField] private Slider slider;

    public int currentHp;
    public int maxHp;

    private void Start()
    {
        currentHp = maxHp;
        UpdateHealthBar();
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

            int currentGoldInWallet = int.Parse(goldText.text);

            goldText.text = (currentGoldInWallet + goldReward).ToString();

        }

        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (slider != null)
        {
            slider.value = (float)currentHp / maxHp;
        }
    }
}
