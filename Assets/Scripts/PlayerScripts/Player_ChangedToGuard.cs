using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player_ChangeToGuard : MonoBehaviour
{
    [Header("References")]
    public Player_Combat combat;
    public Player_Guard guard;
    public Animator playerAnimator;
    public Player_ChangeEquipment changeEquipment;
    public Slider shieldSlider;

    [Header("Settings")]
    public float guardCooldown = 5f;
    public float shieldGracePeriod = 0.4f;

    [Header("State")]
    public bool canSwitchToGuard = true;
    private bool canGuard = true;
    private bool isCooldownActive = false;
    private float speedBeforeGuard;

    public void Start()
    {
        if (shieldSlider != null)
        {
            shieldSlider.maxValue = guardCooldown;
            shieldSlider.value = guardCooldown;
        }
    }

    private void Update()
    {
        if (canGuard && canSwitchToGuard)
        {
            if (Input.GetButtonDown("Guard"))
            {
                StartGuarding();
            }
            if (Input.GetButtonUp("Guard") && guard.enabled)
            {
                StopGuarding();
            }
        }
    }

    private void OnEnable()
    {
        ResetShieldStatus();
    }

    private void ResetShieldStatus()
    {
        isCooldownActive = false;
        canGuard = true;
        StopAllCoroutines();

        if (shieldSlider != null)
        {
            shieldSlider.value = guardCooldown;
        }

        if (guard != null) guard.DeactivateGuard();
        if (combat != null) combat.enabled = true;
    }
    private void StartGuarding()
    {
        speedBeforeGuard = StatsManager.Instance.speed;

        StatsManager.Instance.speed /= 3f;

        combat.enabled = false;
        guard.enabled = true;

        playerAnimator.SetLayerWeight(0, 0f);
        playerAnimator.SetLayerWeight(1, 0f);
        playerAnimator.SetLayerWeight(2, 1f);

        guard.ActivateGuard();
        changeEquipment.canChangeWeapon = false;
    }

    private void StopGuarding()
    {
        StatsManager.Instance.speed = speedBeforeGuard;

        guard.DeactivateGuard();
        guard.enabled = false;
        combat.enabled = true;

        playerAnimator.SetLayerWeight(0, 1f);
        playerAnimator.SetLayerWeight(2, 0f);
        changeEquipment.canChangeWeapon = true;
    }

    public void ResetAfterBlock()
    {
        if (isCooldownActive) return;

        StartCoroutine(DelayedReset());
    }

    IEnumerator DelayedReset()
    {
        yield return new WaitForSeconds(shieldGracePeriod);

        if (!isCooldownActive)
        {
            StopAllCoroutines();
            StartCoroutine(CooldownRoutine());
        }
    }

    IEnumerator CooldownRoutine()
    {
        isCooldownActive = true;
        canGuard = false;

        StopGuarding();

        float timer = 0;
        if (shieldSlider != null) shieldSlider.value = 0;

        while (timer < guardCooldown)
        {
            timer += Time.deltaTime;
            if (shieldSlider != null) shieldSlider.value = timer;
            yield return null;
        }

        if (shieldSlider != null) shieldSlider.value = guardCooldown;

        canGuard = true;
        isCooldownActive = false;
    }

    private void OnDisable()
    {
        if (guard.enabled)
        {
            StatsManager.Instance.speed = speedBeforeGuard;
        }

        isCooldownActive = false;
        canGuard = true;
        StopAllCoroutines();
    }
}