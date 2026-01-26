using UnityEngine;
using TMPro;


public class StatsUI : MonoBehaviour
{
    public GameObject[] statsSlots;
    public CanvasGroup statsCanvas;

    private bool statsOpen=false;

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
                Time.timeScale = 1;
                UpdateAllStats();
                statsCanvas.alpha = 0;
                statsOpen=false;
            }
            else
            {
                Time.timeScale = 0;
                UpdateAllStats();
                statsCanvas.alpha = 1;
                statsOpen = true;
            }  
        }
    }


    public void UpdateStrength()
    {
        statsSlots[0].GetComponentInChildren<TMP_Text>().text = "Strength: " + StatsManager.Instance.damage;
    }
    public void UpdateAgility()
    {
        statsSlots[1].GetComponentInChildren<TMP_Text>().text = "Agility: " + StatsManager.Instance.speed;
    }

    public void UpdateAllStats()
    {
        UpdateStrength();
        UpdateAgility();
    }

}
