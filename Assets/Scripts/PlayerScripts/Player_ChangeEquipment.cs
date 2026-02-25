using UnityEngine;

public class Player_ChangeEquipment : MonoBehaviour
{
    public Player_Combat combat;
    public Player_Bow bow;

    // Fontos: a karakter Animator komponense (általában a Player gyökéren van)
    public Animator playerAnimator;

    // Opcionális: visszajelzéshez
    public AudioSource switchSound;

    private void Awake()
    {
        if (playerAnimator == null)
        {
            playerAnimator = GetComponent<Animator>();
            if (playerAnimator == null)
            {
                Debug.LogError("Player Animator nincs hozzárendelve!");
            }
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("ChangeEquipment"))
        {
            SwitchWeaponMode();
        }
    }

    private void SwitchWeaponMode()
    {
        bool switchingToBow = !bow.enabled;

        // Script-ek ki/be kapcsolása
        combat.enabled = !combat.enabled;
        bow.enabled = !bow.enabled;

        // Layer súlyok beállítása – ez váltja a látható animációt/sprite-ot
        if (switchingToBow)
        {
            // Íj mód → Archer layer aktív, Base (kard) kikapcsol
            playerAnimator.SetLayerWeight(0, 0f);     // Base Layer (melee/kard)
            playerAnimator.SetLayerWeight(1, 1f);     // Archer Layer (íj)
        }
        else
        {
            // Kard mód → Base layer aktív, Archer kikapcsol
            playerAnimator.SetLayerWeight(0, 1f);
            playerAnimator.SetLayerWeight(1, 0f);
        }

        // Extra biztonság: reseteljük az íj cooldown-ját váltáskor
        if (bow.enabled)
        {
            StatsManager.Instance.shootTimer = StatsManager.Instance.shootCooldown;
        }

        // Opcionális visszajelzések
        if (switchSound != null)
        {
            switchSound.Play();
        }

        // Ha van "WeaponSwitch" trigger-ed az animátorban (pl. gyors váltó animációhoz)
        // playerAnimator.SetTrigger("WeaponSwitch");
    }

    // Opcionális: kezdeti állapot biztosítása (pl. karddal indul)
    private void Start()
    {
        // Alapból kard mód
        combat.enabled = true;
        bow.enabled = false;
        playerAnimator.SetLayerWeight(0, 1f);
        playerAnimator.SetLayerWeight(1, 0f);
    }
}