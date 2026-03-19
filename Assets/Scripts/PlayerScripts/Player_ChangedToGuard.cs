using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player_ChangeToGuard : MonoBehaviour
{
    public Player_Combat combat;
    public Player_Guard guard;
    public Animator playerAnimator;
    public Player_ChangeEquipment changeEquipment; // hogy letiltsuk a váltást, amíg Guard van felszerelve
    public Slider shieldSlider;

    public bool canSwitchToGuard = true;
    private bool canGuard = true;
    private float guardCooldown = 5f;
    private bool isCooldownActive = false;

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
        // Ha nincs cooldown, figyeljük a gombot
        if (canGuard)
        {
            if (canSwitchToGuard)
            {
                // GOMB LENYOMÁSA: Belépés Guard módba
            if (Input.GetButtonDown("Guard"))
            {
                    StartGuarding();
                
            }

            // GOMB ELENGEDÉSE: Visszaváltás Combat módba (ha nem ütöttek meg közben)
            if (Input.GetButtonUp("Guard") && guard.enabled)
            {
                StopGuarding();
            }
            }
            
        }
    }

    private void StartGuarding()
    {
        StatsManager.Instance.speed/=3; // Lassulás Guard módban
        combat.enabled = false;
        guard.enabled = true;

        // Rétegek: Guard (2-es index) bekapcsolása
        playerAnimator.SetLayerWeight(0, 0f);
        playerAnimator.SetLayerWeight(1, 0f);
        playerAnimator.SetLayerWeight(2, 1f);

        guard.ActivateGuard();
        changeEquipment.canChangeWeapon = false; // megakadályozzuk a váltást, amíg Guard van felszerelve
    }

    private void StopGuarding()
    {
       StatsManager.Instance.speed*=3; // Vissza a normál sebességre
        guard.DeactivateGuard();
        guard.enabled = false;
        combat.enabled = true;

        // Rétegek: Vissza az alapra
        playerAnimator.SetLayerWeight(0, 1f);
        playerAnimator.SetLayerWeight(2, 0f);
        changeEquipment.canChangeWeapon = true; // visszaengedjük a váltást, ha kikapcsoljuk a Guard-ot
    }

    // Ezt hívja meg a Player_Health, ha megütnek blokkolás közben
    public void ResetAfterBlock()
    {
        if (isCooldownActive) return;
        StartCoroutine(CooldownRoutine());
    }

    IEnumerator CooldownRoutine()
    {
        isCooldownActive = true;
        canGuard = false;

        // Azonnali visszaváltás, mert elhasználtuk a blokkot
        StopGuarding();

        float timer = 0;
        if (shieldSlider != null) shieldSlider.value = 0;

        // Folyamatosan töltjük a csíkot 7 másodpercen keresztül
        while (timer < guardCooldown)
        {
            timer += Time.deltaTime;
            if (shieldSlider != null)
            {
                shieldSlider.value = timer;
            }
            yield return null; // Vár a következõ frame-ig
        }

        if (shieldSlider != null) shieldSlider.value = guardCooldown;

        Debug.Log("Pajzs áttörve! Cooldown: 7mp");

        //yield return new WaitForSeconds(guardCooldown);

        canGuard = true;
        isCooldownActive = false;
        Debug.Log("Pajzs újra kész!");
    }
}