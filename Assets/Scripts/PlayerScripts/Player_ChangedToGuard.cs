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
    public float shieldGracePeriod = 0.4f; // Az Ogre dupla ütése miatt

    [Header("State")]
    public bool canSwitchToGuard = true;
    private bool canGuard = true;
    private bool isCooldownActive = false;
    private float speedBeforeGuard; // Itt tároljuk el a sebességet a lassítás elõtt

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
            // GOMB LENYOMÁSA: Belépés Guard módba
            if (Input.GetButtonDown("Guard"))
            {
                StartGuarding();
            }

            // GOMB ELENGEDÉSE: Visszaváltás Combat módba
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

    // Egy külön függvény, ami tiszta lapot indít a pajzsnak
    private void ResetShieldStatus()
    {
        isCooldownActive = false;
        canGuard = true;
        StopAllCoroutines(); // Megállítunk minden félbemaradt folyamatot

        if (shieldSlider != null)
        {
            shieldSlider.value = guardCooldown;
        }

        // Biztosítjuk, hogy ne maradjon Guard módban vizuálisan
        if (guard != null) guard.DeactivateGuard();
        if (combat != null) combat.enabled = true;
    }
    private void StartGuarding()
    {
        // 1. ELMENTJÜK az aktuális sebességet (potival együtt!), mielõtt lelassítanánk
        speedBeforeGuard = StatsManager.Instance.speed;

        // 2. LEVESSZÜK a sebességet (dinamikusan osztva)
        StatsManager.Instance.speed /= 3f;

        combat.enabled = false;
        guard.enabled = true;

        // Animator rétegek (Guard réteg bekapcsolása)
        playerAnimator.SetLayerWeight(0, 0f);
        playerAnimator.SetLayerWeight(1, 0f);
        playerAnimator.SetLayerWeight(2, 1f);

        guard.ActivateGuard();
        changeEquipment.canChangeWeapon = false;
    }

    private void StopGuarding()
    {
        // 3. FIX VISSZAÁLLÍTÁS: Pontosan azt adjuk vissza, amit elmentettünk
        StatsManager.Instance.speed = speedBeforeGuard;

        guard.DeactivateGuard();
        guard.enabled = false;
        combat.enabled = true;

        // Rétegek visszaállítása az alapra
        playerAnimator.SetLayerWeight(0, 1f);
        playerAnimator.SetLayerWeight(2, 0f);
        changeEquipment.canChangeWeapon = true;
    }

    // Ezt hívja meg a Player_Health, ha megütnek blokkolás közben
    public void ResetAfterBlock()
    {
        if (isCooldownActive) return;

        // Elindítjuk a késleltetett pihenõt, hogy az Ogre második ütése is a pajzsba menjen
        StartCoroutine(DelayedReset());
    }

    IEnumerator DelayedReset()
    {
        // Várunk, amíg az Ogre befejezi a dupla ütést (0.2s és 0.5s között)
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

        // Itt hívjuk meg a Stop-ot, ami visszaállítja a sebességet is!
        StopGuarding();

        float timer = 0;
        if (shieldSlider != null) shieldSlider.value = 0;

        // Slider töltése
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

    // Ha a Player meghal vagy a script kikapcsol, takarítsunk fel!
    private void OnDisable()
    {
        // Ha épp Guardban voltunk, adjuk vissza a sebességet
        if (guard.enabled)
        {
            StatsManager.Instance.speed = speedBeforeGuard;
        }

        isCooldownActive = false;
        canGuard = true;
        StopAllCoroutines();
    }
}