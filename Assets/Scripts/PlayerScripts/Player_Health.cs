using UnityEngine;
using TMPro;

public class Player_Health : MonoBehaviour
{

    public TMP_Text healthText;
    public Animator healthTextAnim;


    private void Start()
    {
        healthText.text = "HP:" + StatsManager.Instance.currentHp + "/" + StatsManager.Instance.maxHp;
    }

    public void ChangeHealth(int amount)
    {
        StatsManager.Instance.currentHp += amount;

        healthTextAnim.Play("HP_Text_Animation");

        healthText.text = "HP:" + StatsManager.Instance.currentHp + "/" + StatsManager.Instance.maxHp;
        if (StatsManager.Instance.currentHp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
