using UnityEngine;

public class Player_ChangeEquipment : MonoBehaviour
{
    public Player_Combat combat;
    public Player_Bow bow;
    public bool canChangeWeapon = true;
    public Animator playerAnimator;

    public AudioSource switchSound;

    private void Awake()
    {
        if (playerAnimator == null)
        {
            playerAnimator = GetComponent<Animator>();
        }
    }

    private void Update()
    {
        if (canChangeWeapon)
        {
            if (Input.GetButtonDown("ChangeEquipment"))
            {
                SwitchWeaponMode();
            }
        }
    }

    private void SwitchWeaponMode()
    {
        bool switchingToBow = !bow.enabled;
        combat.enabled = !combat.enabled;
        bow.enabled = !bow.enabled;
            if (switchingToBow)
            {
                playerAnimator.SetLayerWeight(0, 0f);     
                playerAnimator.SetLayerWeight(1, 1f);    
            }
            else
            {
                playerAnimator.SetLayerWeight(0, 1f);
                playerAnimator.SetLayerWeight(1, 0f);
            }
        
        if (bow.enabled)
        {
            StatsManager.Instance.shootTimer = StatsManager.Instance.shootCooldown;
        }

        if (switchSound != null)
        {
            switchSound.Play();
        }
    }

    private void Start()
    {
        combat.enabled = true;
        bow.enabled = false;
        playerAnimator.SetLayerWeight(0, 1f);
        playerAnimator.SetLayerWeight(1, 0f);
    }
}