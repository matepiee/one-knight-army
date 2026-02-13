using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    public Animator anim;
    public CanvasGroup shopCanvasGroup;

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
                    isShopOpen = true;
                    shopCanvasGroup.alpha = 1;
                    shopCanvasGroup.blocksRaycasts = true;
                    shopCanvasGroup.interactable = true;
                }
                else
                {
                    isShopOpen = false;
                    shopCanvasGroup.alpha = 0;
                    shopCanvasGroup.blocksRaycasts = false;
                    shopCanvasGroup.interactable = false;
                }
                
            }
        }
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
