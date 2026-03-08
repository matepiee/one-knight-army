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
    [Header("Effects")]
    public GameObject deathParticle;

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
        else if (currentHp <= 0)
        {
            OnMonsterDefeated?.Invoke(expReward);

            InventoryManager inv = FindFirstObjectByType<InventoryManager>();
            if (inv != null)
            {
                inv.gold += goldReward;
                inv.goldText.text = inv.gold.ToString();
            }

            if (deathParticle != null)
            {
                Instantiate(deathParticle, transform.position, Quaternion.identity);
            }

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

    private void LateUpdate()
    {
        // Keeps the healthbar from flipping when the enemy turns around
        if (slider != null)
        {
            Vector3 scale = slider.transform.localScale;
            scale.x = transform.localScale.x > 0 ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
            slider.transform.localScale = scale;
        }
    }
}
