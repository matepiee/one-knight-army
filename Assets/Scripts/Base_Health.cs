using UnityEngine;
using TMPro;

public class Base_Health : MonoBehaviour
{
    [Header("Options")]
    public int maxHp = 10;
    public int currentHp;

    [Header("UI Reference")]
    public TMP_Text baseHpText;
    public GameObject GameOverCanvas;
    public CanvasGroup GameOverCanvasGroup;

    public GameObject dieCanvas;
    public CanvasGroup dieCanvasGroup;

    void Start()
    {
        currentHp = maxHp;
        UpdateBaseUI();
    }

    public void TakeDamage(int amount)
    {
        currentHp -= amount;

        if (currentHp < 0) currentHp = 0;

        UpdateBaseUI();

        if (currentHp <= 0)
        {
            GameOver();
        }
    }

    void UpdateBaseUI()
    {
        if (baseHpText != null)
        {
            baseHpText.text = "HP: " + currentHp + " / " + maxHp;
        }
    }

    void GameOver()
    {
        dieCanvas.SetActive(false);
        dieCanvasGroup.alpha = 0;

        GameOverCanvas.SetActive(true);
        GameOverCanvasGroup.alpha = 1;
        GameOverCanvasGroup.blocksRaycasts = true;
        GameOverCanvasGroup.interactable = true;

        Time.timeScale = 0;

        

    }
}