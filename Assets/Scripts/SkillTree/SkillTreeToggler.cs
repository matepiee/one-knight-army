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
<<<<<<< Updated upstream
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
=======
        enter.onClick.AddListener(SkillTreeEnter);
        leave.onClick.AddListener(SkillTreeLeave);

    }
    public void SkillTreeEnter()
    {
        Time.timeScale = 0;
        skillsCanvas.alpha = 1;
        skillsCanvas.blocksRaycasts = true;
        skillsCanvas.interactable = true;
        skillTreeOpen = true;
        statsCanvas.alpha = 0;
        statsCanvas.blocksRaycasts = false;
        statsCanvas.interactable = false;


    }
    public void SkillTreeLeave()
    {
        Time.timeScale = 1;
        skillsCanvas.alpha = 0;
        skillsCanvas.blocksRaycasts = false;
        skillsCanvas.interactable = false;
        skillTreeOpen = false;
        statsCanvas.alpha = 1;
        statsCanvas.blocksRaycasts = true;
        statsCanvas.interactable = true;
>>>>>>> Stashed changes

    }


    
}
