using UnityEngine;

public class MainMenuOpenSettings : MonoBehaviour
{
    public CanvasGroup settingsCanvasGroup;
    public GameObject settingsCanvas;
    void Start()
    {
        if (settingsCanvas != null)
        {
            settingsCanvas.SetActive(false);
        }
    }

    
    void Update()
    {
        
    }

    public void OpenSettings()
    {
        settingsCanvas.SetActive(true);
        settingsCanvasGroup.alpha = 1;
        settingsCanvasGroup.blocksRaycasts = true;
        settingsCanvasGroup.interactable = true;
    }

    public void CloseSettings()
    {
        settingsCanvas.SetActive(false);
        settingsCanvasGroup.alpha = 0;
        settingsCanvasGroup.blocksRaycasts = false;
        settingsCanvasGroup.interactable = false;
    }
}
