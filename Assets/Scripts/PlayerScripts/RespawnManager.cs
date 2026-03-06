using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager Instance;

    [Header("UI beállítások")]
    public TMP_Text timerText;
    public GameObject dieCanvas;
    public CanvasGroup dieCanvasGroup;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void RespawnPlayer(GameObject player, float delay, Vector3 spawnPos)
    {
        StartCoroutine(RespawnRoutine(player, delay, spawnPos));
    }

    IEnumerator RespawnRoutine(GameObject player, float delay, Vector3 spawnPos)
    {
        player.SetActive(false);

        if (dieCanvas != null) dieCanvas.SetActive(true);
        if (dieCanvasGroup != null) dieCanvasGroup.alpha = 0;

        float remainingTime = delay;

        while (remainingTime > 0)
        {
            if (dieCanvasGroup != null && dieCanvasGroup.alpha < 1)
            {
                dieCanvasGroup.alpha += Time.deltaTime * 2f;
            }

            if (timerText != null)
            {
                timerText.text = "Respawn: " + remainingTime.ToString("f1") + "s";
            }

            remainingTime -= Time.deltaTime;
            yield return null;
        }

        if (dieCanvasGroup != null) dieCanvasGroup.alpha = 0;
        if (dieCanvas != null) dieCanvas.SetActive(false);

        if (timerText != null) timerText.text = "";


        player.transform.position = spawnPos;
        Player_Movement pm = player.GetComponent<Player_Movement>();
        if (pm != null) pm.ResetMovement();
        StatsManager.Instance.currentHp = StatsManager.Instance.maxHp;
        player.SetActive(true);
    }
}