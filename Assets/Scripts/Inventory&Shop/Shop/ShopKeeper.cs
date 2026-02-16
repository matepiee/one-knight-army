using JetBrains.Annotations;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    public Animator anim;
    public CanvasGroup shopCanvasGroup;
    public ShopManager shopManager;

    private bool playerinrange;
    public bool isShopOpen;

    void Update()
    {
        if (playerinrange)
        {
            if (Input.GetButtonDown("Interact"))
            {
                if (!isShopOpen)
                {
                    Time.timeScale = 0;
                    isShopOpen = true;
                    shopCanvasGroup.alpha = 1;
                    shopCanvasGroup.blocksRaycasts = true;
                    shopCanvasGroup.interactable = true;

                }
                else
                {
                    Time.timeScale = 1;
                    isShopOpen = false;
                    shopCanvasGroup.alpha = 0;
                    shopCanvasGroup.blocksRaycasts = false;
                    shopCanvasGroup.interactable = false;
                }

                if (shopManager != null)
                {
                    shopManager.ToggleShop(isShopOpen);
                }
            }
        }
    }

    public void OpenItemShop()
    {

    }

    public void OpenPotionShop()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("playerinrange", true);
            playerinrange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("playerinrange", false);
            playerinrange = false;
            isShopOpen = false;
            shopCanvasGroup.alpha = 0;
            shopCanvasGroup.blocksRaycasts = false;
            shopCanvasGroup.interactable = false;

            if (shopManager != null)
            {
                shopManager.ToggleShop(false);
            }
        }
    }
}
