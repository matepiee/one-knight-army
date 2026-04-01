using UnityEngine;
using TMPro;

public class StatsUI : MonoBehaviour
{
    public GameObject[] statsSlots;
    public CanvasGroup statsCanvas;
    public bool statsOpen = false;

    private void Start() => UpdateAllStats();

    public void OpenStats()
    {
        if (statsOpen) return;
        statsOpen = true;
        UIManager.OpenWindowCount++;
        Time.timeScale = 0;

        AudioManager.instance.Play("PopUpOpen");

        UpdateAllStats();
        statsCanvas.alpha = 1;
        statsCanvas.interactable = true;
        statsCanvas.blocksRaycasts = true;
    }

    public void CloseStats()
    {
        if (!statsOpen) return;
        statsOpen = false;
        UIManager.OpenWindowCount--;
        Time.timeScale = 1;

        AudioManager.instance.Play("PopUpClose");

        statsCanvas.alpha = 0;
        statsCanvas.interactable = false;
        statsCanvas.blocksRaycasts = false;
    }

    public void UpdateAllStats()
    {
        statsSlots[0].GetComponentInChildren<TMP_Text>().text = "Strength: " + StatsManager.Instance.damage;
        statsSlots[1].GetComponentInChildren<TMP_Text>().text = "Agility: " + StatsManager.Instance.speed.ToString("0.##");
        statsSlots[2].GetComponentInChildren<TMP_Text>().text = "Vitality: " + StatsManager.Instance.maxHp / 10;
    }
}