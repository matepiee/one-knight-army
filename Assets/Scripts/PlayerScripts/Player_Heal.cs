using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player_Heal : MonoBehaviour
{
    private float healCooldown = 10f;
    public Slider healSlider;
    public CanvasGroup healSliderGroup;
    private bool isCooldownActive = false;
    private bool canheal = true;

    public void Start()
    {
        ResetHealStatus();
    }

    // ÚJRAÉLEDÉS KEZELÉSE
    private void OnEnable()
    {
        ResetHealStatus();
    }

    private void ResetHealStatus()
    {
        isCooldownActive = false;
        canheal = true;
        StopAllCoroutines(); // Megállítja a beragadt folyamatokat

        if (healSlider != null)
        {
            healSlider.maxValue = healCooldown;
            healSlider.value = 0;
            healSliderGroup.alpha = 0;
        }
    }

    public void Heal()
    {
        if (canheal)
        {
            StatsManager.Instance.UpdateHealth(StatsManager.Instance.heal);
            ResetAfterHeal();
            healSliderGroup.alpha = 1;
        }
    }

    public void ResetAfterHeal()
    {
        if (isCooldownActive) return;
        StartCoroutine(CooldownRoutine());
    }

    IEnumerator CooldownRoutine()
    {
        isCooldownActive = true;
        canheal = false;

        float timer = 0;
        // Itt a csúszka feltöltõdik, majd eltûnik
        while (timer < healCooldown)
        {
            timer += Time.deltaTime;
            if (healSlider != null) healSlider.value = timer;
            yield return null;
        }

        // Visszaállítás a végén
        if (healSlider != null) healSlider.value = 0;
        healSliderGroup.alpha = 0;

        canheal = true;
        isCooldownActive = false;
    }
}