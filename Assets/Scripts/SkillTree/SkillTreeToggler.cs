using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SkillTreeToggler : MonoBehaviour
{
    public CanvasGroup skillsCanvas;
    private bool skillTreeOpen=false;
    public Button button;
    public void SkillTreeToggle()
    {
        if (skillTreeOpen)
        {
            Time.timeScale = 1;
            skillsCanvas.alpha = 0;
            skillsCanvas.blocksRaycasts = false;
            skillsCanvas.interactable=true;
            skillTreeOpen = false;
        }
        else {
            Time.timeScale = 0;
            skillsCanvas.alpha = 1;
            skillsCanvas.blocksRaycasts = true;
            skillsCanvas.interactable = true;
            skillTreeOpen = true;
        }

    }


    
}
