using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseCanvas;
    public CanvasGroup pauseCanvasGroup;
    public GameObject settingsCanvas;
    public CanvasGroup settingsCanvasGroup;

    public ShopKeeper shopKeeper;
    public StatsUI statsUI;
    public SkillTreeToggler skillTree;

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
        if (Input.GetButtonDown("Pause")) // Ez az Escape nálad
        {
            // 1. PRIORITÁS: Ha a Settings (vagy bármilyen belsõ ablak) nyitva van
            if (settingsCanvas.activeSelf)
            {
                CloseSettings();
                return; // Megállunk, nem fut tovább a kód
            }

            // 2. PRIORITÁS: Ha a Global UI (Shop vagy Stats) nyitva van
            if (UIManager.IsAnyUIOpen)
            {
                if (shopKeeper != null && shopKeeper.isShopOpen)
                {
                    shopKeeper.CloseShop();
                }
                else if (statsUI != null && statsUI.statsOpen)
                {
                    statsUI.CloseStats();
                }
                else if (skillTree != null && skillTree.skillTreeOpen)
                {
                    skillTree.SkillTreeLeave();
                }
                return;
            }

            // 3. PRIORITÁS: Ha semmi nincs nyitva, jöhet a Pause/Resume
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseCanvas.SetActive(true);
        pauseCanvasGroup.alpha = 1;
        pauseCanvasGroup.blocksRaycasts = true;
        pauseCanvasGroup.interactable = true;
        Time.timeScale = 0;
        isPaused = true;
    }
    public void ResumeGame()
    {
        pauseCanvas.SetActive(false);
        pauseCanvasGroup.alpha = 0;
        pauseCanvasGroup.blocksRaycasts = false;
        pauseCanvasGroup.interactable = false;
        Time.timeScale = 1;
        isPaused = false;
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void StartOver()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
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
