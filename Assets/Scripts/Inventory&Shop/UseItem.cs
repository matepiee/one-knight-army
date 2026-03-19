using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UseItem : MonoBehaviour
{
    [Header("Potion UI Sliders")]
    public Slider swiftnessSlider;
    public Slider maxHpSlider;

    private float swiftnessDurationLeft = 0f;
    private float swiftnessMaxDuration = 1f;
    
    private float maxHpDurationLeft = 0f;
    private float maxHpMaxDuration = 1f;

    private void Start()
    {
        if (swiftnessSlider != null) swiftnessSlider.gameObject.SetActive(false);
        if (maxHpSlider != null) maxHpSlider.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Update Swiftness Slider
        if (swiftnessDurationLeft > 0)
        {
            swiftnessDurationLeft -= Time.deltaTime;
            if (swiftnessSlider != null) swiftnessSlider.value = swiftnessDurationLeft / swiftnessMaxDuration;
        }
        else if (swiftnessSlider != null && swiftnessSlider.gameObject.activeSelf)
        {
            swiftnessSlider.gameObject.SetActive(false);
        }

        // Update Max HP Slider
        if (maxHpDurationLeft > 0)
        {
            maxHpDurationLeft -= Time.deltaTime;
            if (maxHpSlider != null) maxHpSlider.value = maxHpDurationLeft / maxHpMaxDuration;
        }
        else if (maxHpSlider != null && maxHpSlider.gameObject.activeSelf)
        {
            maxHpSlider.gameObject.SetActive(false);
        }
    }

    public void ApplyItemEffects(ItemSO itemSO)
    {
        if (itemSO.currentHealth > 0)
            StatsManager.Instance.UpdateHealth(itemSO.currentHealth);
        if (itemSO.maxHealth > 0)
            StatsManager.Instance.UpdateMaxHealth(itemSO.maxHealth);
        if (itemSO.speed > 0)
            StatsManager.Instance.UpdateSpeed(itemSO.speed);
        if(itemSO.damage > 0)
            StatsManager.Instance.UpdateAttackDamage(itemSO.damage);
        if (itemSO.duration > 0)
        {
            StartCoroutine(EffectTimer(itemSO, itemSO.duration));

            // UI Sliders logic
            if (itemSO.speed > 0 && swiftnessSlider != null)
            {
                swiftnessDurationLeft = itemSO.duration;
                swiftnessMaxDuration = itemSO.duration;
                swiftnessSlider.gameObject.SetActive(true);
            }
            if (itemSO.maxHealth > 0 && maxHpSlider != null)
            {
                maxHpDurationLeft = itemSO.duration;
                maxHpMaxDuration = itemSO.duration;
                maxHpSlider.gameObject.SetActive(true);
            }
        }
    }

    private IEnumerator EffectTimer(ItemSO itemSO, float duration)
    {
        yield return new WaitForSeconds(duration);

        if (itemSO.currentHealth > 0)
            StatsManager.Instance.UpdateHealth(-itemSO.currentHealth);
        if (itemSO.maxHealth > 0)
            StatsManager.Instance.UpdateMaxHealth(-itemSO.maxHealth);
        if (itemSO.speed > 0)
            StatsManager.Instance.UpdateSpeed(-itemSO.speed);
        if (itemSO.damage > 0)
            StatsManager.Instance.UpdateAttackDamage(-itemSO.damage);
    }
}
