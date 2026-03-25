using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;

public class SkillTreeToggler : MonoBehaviour
{
    public CanvasGroup skillsCanvas;
    public CanvasGroup statsCanvas;
    public bool skillTreeOpen=false;
    public Button enter;
    public Button leave;
   
    private void Start()
    {
        enter.onClick.AddListener(SkillTreeEnter);
        leave.onClick.AddListener(SkillTreeLeave);
    }
    public void SkillTreeEnter()
    {
        if (skillTreeOpen) return;

        UIManager.OpenWindowCount++;
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
        if (!skillTreeOpen) return;

        UIManager.OpenWindowCount--;
        Time.timeScale = 1;
        skillsCanvas.alpha = 0;
        skillsCanvas.blocksRaycasts = false;
        skillsCanvas.interactable = false;
        skillTreeOpen = false;
        statsCanvas.alpha = 1;
        statsCanvas.blocksRaycasts = true;
        statsCanvas.interactable = true;

    }

}
