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

    private void OnEnable()
    {
        ResetHealStatus();
    }

    private void ResetHealStatus()
    {
        isCooldownActive = false;
        canheal = true;
        StopAllCoroutines();

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
        while (timer < healCooldown)
        {
            timer += Time.deltaTime;
            if (healSlider != null) healSlider.value = timer;
            yield return null;
        }

        if (healSlider != null) healSlider.value = 0;
        healSliderGroup.alpha = 0;

        canheal = true;
        isCooldownActive = false;
    }
}