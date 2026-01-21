using UnityEngine;
using TMPro;

public class Player_Health : MonoBehaviour
{
    public int currentHp;
    public int maxHp;

    public TMP_Text healthText;
    public Animator healthTextAnim;


    private void Start()
    {
        healthText.text = "HP:" + currentHp + "/" + maxHp;
    }

    public void ChangeHealth(int amount)
    {
        currentHp += amount;

        healthTextAnim.Play("HP_Text_Animation");

        healthText.text = "HP:" + currentHp + "/" + maxHp;
        if (currentHp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
