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

        if (MusicManager.instance != null)
        {
            MusicManager.instance.PlayPreparationMusic();
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (shopKeeper != null && shopKeeper.playerinrange)
            {
                if (shopKeeper.isShopOpen) shopKeeper.CloseShop();
                else if (!isPaused && !UIManager.IsAnyUIOpen) shopKeeper.OpenShop();
            }
        }

        if (Input.GetButtonDown("ToggleStats"))
        {
            if (statsUI.statsOpen && !skillTree.skillTreeOpen)
            {
                statsUI.CloseStats();
            }
            else if (!isPaused && !UIManager.IsAnyUIOpen)
            {
                statsUI.OpenStats();
            }
        }

        if (Input.GetButtonDown("Pause"))
        {
            if (settingsCanvas != null && settingsCanvas.activeSelf)
            {
                CloseSettings();
                return;
            }

            if (skillTree != null && skillTree.skillTreeOpen)
            {
                skillTree.SkillTreeLeave();
                return;
            }

            if (statsUI != null && statsUI.statsOpen)
            {
                statsUI.CloseStats();
                return;
            }

            if (shopKeeper != null && shopKeeper.isShopOpen)
            {
                shopKeeper.CloseShop();
                return;
            }

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
        AudioManager.instance.Play("PopUpOpen");

        MusicManager.instance.PlayPauseMusic();
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

        AudioManager.instance.Play("PopUpClose");

        MusicManager.instance.ResumePreviousMusic();
    }

    public void OpenSettings()
    {
        if (settingsCanvas != null) settingsCanvas.SetActive(true);
        settingsCanvasGroup.alpha = 1;
        settingsCanvasGroup.blocksRaycasts = true;
        settingsCanvasGroup.interactable = true;

        AudioManager.instance.Play("PopUpOpen");
    }

    public void CloseSettings()
    {
        if (settingsCanvas != null) settingsCanvas.SetActive(false);
        settingsCanvasGroup.alpha = 0;
        settingsCanvasGroup.blocksRaycasts = false;
        settingsCanvasGroup.interactable = false;

        AudioManager.instance.Play("PopUpClose");
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
}