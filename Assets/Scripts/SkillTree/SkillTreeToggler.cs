using UnityEngine;
using UnityEngine.UI;

public class SkillTreeToggler : MonoBehaviour
{
    public CanvasGroup skillsCanvas;
    public CanvasGroup statsCanvas;
    public bool skillTreeOpen = false;
    public Button enter;
    public Button leave;

    private void Start()
    {
        if (enter != null) enter.onClick.AddListener(SkillTreeEnter);
        if (leave != null) leave.onClick.AddListener(SkillTreeLeave);
    }

    public void SkillTreeEnter()
    {
        if (skillTreeOpen) return;
        skillTreeOpen = true;
        UIManager.OpenWindowCount++;

        AudioManager.instance.Play("PopUpOpen");

        skillsCanvas.alpha = 1;
        skillsCanvas.blocksRaycasts = true;
        skillsCanvas.interactable = true;

        statsCanvas.alpha = 0;
        statsCanvas.blocksRaycasts = false;
        statsCanvas.interactable = false;
    }

    public void SkillTreeLeave()
    {
        if (!skillTreeOpen) return;
        skillTreeOpen = false;
        UIManager.OpenWindowCount--;

        AudioManager.instance.Play("PopUpClose");

        skillsCanvas.alpha = 0;
        skillsCanvas.blocksRaycasts = false;
        skillsCanvas.interactable = false;

        statsCanvas.alpha = 1;
        statsCanvas.blocksRaycasts = true;
        statsCanvas.interactable = true;
    }
}