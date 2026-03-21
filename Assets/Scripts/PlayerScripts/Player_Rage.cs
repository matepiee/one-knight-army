using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player_Rage : MonoBehaviour
{
    [Header("Duration UI (Aktív idõ)")]
    public Slider durationSlider;
    public CanvasGroup durationGroup;
    public float invincibilityDuration = 5f;

    [Header("Cooldown UI (Várakozási idõ)")]
    public Slider cooldownSlider;
    public CanvasGroup cooldownGroup;
    public float invincibilityCooldown = 30f;

    private float durationLeft = 0f;
    private bool isCooldownActive = false;
    public bool isInvincible = false;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // Alapból minden rejtve
        if (durationGroup != null) durationGroup.alpha = 0;
        if (cooldownGroup != null) cooldownGroup.alpha = 0;

        if (durationSlider != null) durationSlider.maxValue = invincibilityDuration;
        if (cooldownSlider != null) cooldownSlider.maxValue = invincibilityCooldown;
    }

    private void Update()
    {
        // 1. DURATION CSÍK KEZELÉSE (Fogyó csík)
        if (durationLeft > 0)
        {
            durationLeft -= Time.deltaTime;
            if (durationSlider != null) durationSlider.value = durationLeft;
        }
        else if (isInvincible)
        {
            StopInvincibility();
        }
    }

    public void UseSkill()
    {
        if (!isInvincible && !isCooldownActive)
        {
            StartInvincibility();
        }
    }

    private void StartInvincibility()
    {
        isInvincible = true;
        durationLeft = invincibilityDuration;

        if (durationGroup != null) durationGroup.alpha = 1;
        if (spriteRenderer != null) spriteRenderer.color = new Color(0.6f, 0f, 0f, 0.9f); // Sárgás fény
    }

    private void StopInvincibility()
    {
        isInvincible = false;
        if (durationGroup != null) durationGroup.alpha = 0;
        if (spriteRenderer != null) spriteRenderer.color = Color.white;

        StartCoroutine(CooldownRoutine());
    }

    IEnumerator CooldownRoutine()
    {
        isCooldownActive = true;
        if (cooldownGroup != null) cooldownGroup.alpha = 1;

        float timer = 0;
        while (timer < invincibilityCooldown)
        {
            timer += Time.deltaTime;
            if (cooldownSlider != null)
            {
                // A cooldown csík töltõdjön fel (mint a healnél)
                cooldownSlider.value = timer;
            }
            yield return null;
        }

        if (cooldownGroup != null) cooldownGroup.alpha = 0;
        isCooldownActive = false;
    }
}