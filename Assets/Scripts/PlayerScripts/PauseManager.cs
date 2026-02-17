using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseCanvas;
    public CanvasGroup pauseCanvasGroup;
    public GameObject settingsCanvas;
    public CanvasGroup settingsCanvasGroup;

    public bool isPaused = false;
    void Start()
    {
        if(pauseCanvas != null)
        {
            pauseCanvas.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (isPaused)
            {
                ResumeGame();
                pauseCanvasGroup.alpha = 0;
                pauseCanvasGroup.blocksRaycasts = false;
                pauseCanvasGroup.interactable = false;
            }
            else
            {
                PauseGame();
                pauseCanvasGroup.alpha = 1;
                pauseCanvasGroup.blocksRaycasts = true;
                pauseCanvasGroup.interactable = true;
            }
        }
    }

    public void PauseGame()
    {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }
    public void ResumeGame()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
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
