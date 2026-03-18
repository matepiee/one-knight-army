using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    public Animator anim;
    public Animator shopUIAnimator;
    public CanvasGroup shopCanvasGroup;
    public ShopManager shopManager;

    [SerializeField] private List<ShopItems> shopItems;
    [SerializeField] private List<ShopItems> shopPotions;

    public float animationTime = 1f;
    private bool isAnimating = false;

    public static event Action<ShopManager, bool> OnShopStateChanged;

    private bool playerinrange;
    public bool isShopOpen;

    void Update()
    {
        if (playerinrange)
        {
            if (Input.GetButtonDown("Interact") && !isAnimating)
            {
                StartCoroutine(ToggleShopRoutine());
            }
        }
    }

    private IEnumerator ToggleShopRoutine()
    {
        isAnimating = true;

        if (!isShopOpen)
        {
            isShopOpen = true;
            Time.timeScale = 0;
            
            OnShopStateChanged?.Invoke(shopManager, true);
            shopCanvasGroup.alpha = 1;
            shopCanvasGroup.blocksRaycasts = true;
            shopCanvasGroup.interactable = true;
            OpenPotionShop();

            if (shopUIAnimator != null)
            {
                shopUIAnimator.SetTrigger("slidein");
            }

            yield return new WaitForSecondsRealtime(animationTime);
        }
        else
        {
            Time.timeScale = 1;
            isShopOpen = false;
            
            OnShopStateChanged?.Invoke(shopManager, false);
            shopCanvasGroup.blocksRaycasts = false;
            shopCanvasGroup.interactable = false;

            if (shopUIAnimator != null)
            {
                shopUIAnimator.SetTrigger("slideout");
            }

            yield return new WaitForSecondsRealtime(animationTime);

            shopCanvasGroup.alpha = 0;
        }

        isAnimating = false;
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

            if (isShopOpen)
            {
                Time.timeScale = 1;
                isShopOpen = false;
                OnShopStateChanged?.Invoke(shopManager, false);
            }
            /*
            shopCanvasGroup.alpha = 0;
            shopCanvasGroup.blocksRaycasts = false;
            shopCanvasGroup.interactable = false;
            */
        }
    }
}
