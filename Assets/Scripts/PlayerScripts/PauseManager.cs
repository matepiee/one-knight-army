using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("Canvases")]
    public GameObject pauseCanvas;
    public CanvasGroup pauseCanvasGroup;
    public GameObject settingsCanvas;
    public CanvasGroup settingsCanvasGroup;

    [Header("Scripts")]
    public StatsUI statsUI;
    public SkillTreeToggler skillTree;
    public ShopKeeper shopKeeper;

    [Header("State")]
    public bool isPaused = false;

    private void Start()
    {
        settingsCanvas.SetActive(false);
        pauseCanvas.SetActive(false);
    }
    void Update()
    {
        // 1. SHOP (E / Interact)
        if (Input.GetButtonDown("Interact"))
        {
            if (shopKeeper != null && shopKeeper.playerinrange)
            {
                if (shopKeeper.isShopOpen) shopKeeper.CloseShop();
                else if (!isPaused && !UIManager.IsAnyUIOpen) shopKeeper.OpenShop();
            }
        }

        // 2. STATS (T / ToggleStats)
        if (Input.GetButtonDown("ToggleStats"))
        {
            // Csak akkor zárjuk a T-vel, ha a Skill Tree nincs nyitva alatta
            if (statsUI.statsOpen && !skillTree.skillTreeOpen)
            {
                statsUI.CloseStats();
            }
            else if (!isPaused && !UIManager.IsAnyUIOpen)
            {
                statsUI.OpenStats();
            }
        }

        // 3. ESCAPE (Prioritási sorrend)
        if (Input.GetButtonDown("Pause"))
        {
            // Settings bezárása
            if (settingsCanvas != null && settingsCanvas.activeSelf)
            {
                CloseSettings();
                return;
            }

            // Skill Tree bezárása (Visszalép a Stats-ba)
            if (skillTree != null && skillTree.skillTreeOpen)
            {
                skillTree.SkillTreeLeave();
                return;
            }

            // Stats bezárása
            if (statsUI != null && statsUI.statsOpen)
            {
                statsUI.CloseStats();
                return;
            }

            // Shop bezárása
            if (shopKeeper != null && shopKeeper.isShopOpen)
            {
                shopKeeper.CloseShop();
                return;
            }

            // Ha semmi más nincs nyitva -> Pause/Resume
            if (isPaused) ResumeGame();
            else if (!UIManager.IsAnyUIOpen) PauseGame();
        }
    }


    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        pauseCanvas.SetActive(true);
        if (pauseCanvasGroup != null)
        {
            pauseCanvasGroup.alpha = 1;
            pauseCanvasGroup.blocksRaycasts = true;
            pauseCanvasGroup.interactable = true;
        }
    }

    public void ResumeGame()
    {
        if (settingsCanvas != null) settingsCanvas.SetActive(false);

        isPaused = false;
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
        if (pauseCanvasGroup != null)
        {
            pauseCanvasGroup.alpha = 0;
            pauseCanvasGroup.blocksRaycasts = false;
            pauseCanvasGroup.interactable = false;
        }
    }

    public void OpenSettings()
    {
        if (settingsCanvas != null) settingsCanvas.SetActive(true);
        settingsCanvasGroup.alpha = 1;
        settingsCanvasGroup.blocksRaycasts = true;
        settingsCanvasGroup.interactable = true;
    }

    public void CloseSettings()
    {
        if (settingsCanvas != null) settingsCanvas.SetActive(false);
        settingsCanvasGroup.alpha = 0;
        settingsCanvasGroup.blocksRaycasts = false;
        settingsCanvasGroup.interactable = false;
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}