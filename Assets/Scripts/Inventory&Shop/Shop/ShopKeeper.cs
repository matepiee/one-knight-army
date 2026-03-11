using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    [Header("References")]
    public Animator anim;
    public Animator shopAnim;
    public CanvasGroup shopCanvasGroup;
    public ShopManager shopManager;

    [Header("Shop Settings")]
    [SerializeField] private List<ShopItems> shopItems;
    [SerializeField] private List<ShopItems> shopPotions;

    public static event Action<ShopManager, bool> OnShopStateChanged;

    private bool playerinrange;
    private bool isAnimating;
    public bool isShopOpen;

    public Animator shopAnimator;

    void Update()
    {
        if (playerinrange && !isAnimating)
        {
            if (Input.GetButtonDown("Interact"))
            {
                if (!isShopOpen)
                {
<<<<<<< Updated upstream
                    OpenShop();
                }
                else
                {
                    CloseShop();
=======
                    StartCoroutine(OpenShopRoutine());
                }
                else
                {
                    StartCoroutine(CloseShopRoutine());
>>>>>>> Stashed changes
                }
            }
        }
    }

<<<<<<< Updated upstream
    private void OpenShop()
    {
=======
    private IEnumerator OpenShopRoutine()
    {
        isAnimating = true;
>>>>>>> Stashed changes
        isShopOpen = true;

        shopCanvasGroup.alpha = 1;
        shopCanvasGroup.blocksRaycasts = true;
        shopCanvasGroup.interactable = true;

<<<<<<< Updated upstream
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

=======
        OpenPotionShop();
        OnShopStateChanged?.Invoke(shopManager, true);

        if (shopAnim != null)
        {
            shopAnim.SetTrigger("SlideIn");
        }

        yield return new WaitForSecondsRealtime(0.5f);

        Time.timeScale = 0;
        isAnimating = false; 
    }

    private IEnumerator CloseShopRoutine()
    {
        isAnimating = true; 

        Time.timeScale = 1; 
        isShopOpen = false;
        OnShopStateChanged?.Invoke(shopManager, false);


        if (shopAnim != null)
        {
            shopAnim.SetTrigger("SlideOut");
        }

        yield return new WaitForSeconds(0.5f);

        DisableCanvas();

        isAnimating = false;
    }

    private void DisableCanvas()
    {
>>>>>>> Stashed changes
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
            if (anim != null) anim.SetBool("playerinrange", true);
            playerinrange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (anim != null) anim.SetBool("playerinrange", false);
            playerinrange = false;

            isShopOpen = false;
<<<<<<< Updated upstream
            shopCanvasGroup.alpha = 0;
            shopCanvasGroup.blocksRaycasts = false;
            shopCanvasGroup.interactable = false;
=======
            Time.timeScale = 1;
            DisableCanvas();
>>>>>>> Stashed changes
        }
    }
}