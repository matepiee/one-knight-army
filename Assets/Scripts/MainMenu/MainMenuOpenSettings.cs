using UnityEngine;

public class MainMenuOpenSettings : MonoBehaviour
{
    public CanvasGroup settingsCanvasGroup;
    public GameObject settingsCanvas;
    public GameObject startButton;
    public GameObject settingsButton;
    public GameObject quitButton;
    void Start()
    {
        if (settingsCanvas != null)
        {
            settingsCanvas.SetActive(false);
        }
    }


    public void OpenSettings()
    {
        settingsCanvas.SetActive(true);
        settingsCanvasGroup.alpha = 1;
        settingsCanvasGroup.blocksRaycasts = true;
        settingsCanvasGroup.interactable = true;
        startButton.SetActive(false);
        settingsButton.SetActive(false);
        quitButton.SetActive(false);
    }

    public void CloseSettings()
    {
        settingsCanvas.SetActive(false);
        settingsCanvasGroup.alpha = 0;
        settingsCanvasGroup.blocksRaycasts = false;
        settingsCanvasGroup.interactable = false;
        startButton.SetActive(true);
        settingsButton.SetActive(true);
        quitButton.SetActive(true);
    }
}
