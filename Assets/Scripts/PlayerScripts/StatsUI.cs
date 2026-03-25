using UnityEngine;
using TMPro;


public class StatsUI : MonoBehaviour
{
    public GameObject[] statsSlots;
    public CanvasGroup statsCanvas;

    public bool statsOpen=false;

    private void Start()
    {
        UpdateAllStats();
        
    }

    private void Update()
    {
        if (Input.GetButtonDown("ToggleStats"))
        {
            if (statsOpen)
            {
                CloseStats();
            }
            else
            {
                OpenStats();
            }
        }
    }

    public void OpenStats()
    {
        if (statsOpen) return;

        UIManager.OpenWindowCount++; // NÖVELJÜK a számlálót
        Time.timeScale = 0;
        UpdateAllStats();
        statsCanvas.alpha = 1;
        statsCanvas.interactable = true;
        statsCanvas.blocksRaycasts = true;
        statsOpen = true;
    }

    public void CloseStats()
    {
        if (!statsOpen) return;

        UIManager.OpenWindowCount--; // CSÖKKENTJÜK a számlálót
        Time.timeScale = 1;
        statsCanvas.alpha = 0;
        statsCanvas.interactable = false;
        statsCanvas.blocksRaycasts = false;
        statsOpen = false;
    }
    public void UpdateStrength()
    {
        statsSlots[0].GetComponentInChildren<TMP_Text>().text = "Strength: " + StatsManager.Instance.damage;
    }
    public void UpdateAgility()
    {
        statsSlots[1].GetComponentInChildren<TMP_Text>().text = "Agility: " + StatsManager.Instance.speed.ToString("0.##");
    }
    public void UpdateVitality()
    {
        statsSlots[2].GetComponentInChildren<TMP_Text>().text = "Vitality: " + StatsManager.Instance.maxHp/10;
    }

    public void UpdateAllStats()
    {
        UpdateStrength();
        UpdateAgility();
        UpdateVitality();
    }

}
