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

        if (goldText == null)
        {
            GameObject findtext = GameObject.Find("AmountText");

            if (findtext != null)
            {
                goldText = findtext.GetComponent<TextMeshProUGUI>();
            }
        }
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
            OnMonsterDefeated?.Invoke(expReward);

            int currentGoldInWallet = int.Parse(goldText.text);
            goldText.text = (currentGoldInWallet + goldReward).ToString();

            Destroy(gameObject);
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
