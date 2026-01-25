using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        // Itt a 1-es a Build Settingsben lévõ következõ scene indexe
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("Kilépés...");
        Application.Quit();
    }
}