using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public ItemSO itemSO;
    public int quantity;

    public Image itemImage;
    public TMP_Text quantityText;

    public InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GetComponent<InventoryManager>();        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(quantity > 0)
        {
            if(eventData.button == PointerEventData.InputButton.Left)
            {
                inventoryManager.UseItem(this);
            }
        }
    }

    public void UpdateUI()
    {
        if(itemSO != null)
        {
            itemImage.sprite = itemSO.icon;
            itemImage.gameObject.SetActive(true);
            quantityText.text = quantity.ToString();
        }
        else
        {
            itemImage.gameObject.SetActive(false);
            quantityText.text = "";
        }
        
    }
}
