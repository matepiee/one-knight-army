using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    public Animator anim;
    public CanvasGroup shopCanvasGroup;
    public ShopManager shopManager;

    [SerializeField] private List<ShopItems> shopItems;
    [SerializeField] private List<ShopItems> shopPotions;

    public static event Action<ShopManager, bool> OnShopStateChanged;

    private bool playerinrange;
    public bool isShopOpen;

    public Animator shopAnimator;

    void Update()
    {
        if (playerinrange)
        {
            if (Input.GetButtonDown("Interact"))
            {
                if (!isShopOpen)
                {
                    OpenShop();
                }
                else
                {
                    CloseShop();
                }
            }
        }
    }

    private void OpenShop()
    {
        isShopOpen = true;

        shopCanvasGroup.alpha = 1;
        shopCanvasGroup.blocksRaycasts = true;
        shopCanvasGroup.interactable = true;

        if (shopAnimator != null)
        {
            shopAnimator.SetTrigger("SlideIn");
        }
        Time.timeScale = 0;
        OpenPotionShop();
    }

    private void CloseShop()
    {
        Time.timeScale = 1;
        isShopOpen = false;
        OnShopStateChanged?.Invoke(shopManager, false);

        if (shopAnimator != null)
        {
            shopAnimator.SetTrigger("SlideOut");
        }

        StartCoroutine(DisableCanvasAfterAnimation());
    }

    private System.Collections.IEnumerator DisableCanvasAfterAnimation()
    {
        yield return new WaitForSeconds(0.5f);

        shopCanvasGroup.alpha = 0;
        shopCanvasGroup.blocksRaycasts = false;
        shopCanvasGroup.interactable = false;
    }

    public void OpenItemShop()
    {
        shopManager.PopulateShopItems(shopItems);
    }

    public void OpenPotionShop()
    {
        shopManager.PopulateShopItems(shopPotions);
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
        }
    }
}
