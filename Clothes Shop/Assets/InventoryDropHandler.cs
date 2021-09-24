using ClothesShop.SO.Inventory;
using ClothesShop.UI.Menus;
using ClothesShop.UI.Menus.Prefabs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryDropHandler : MonoBehaviour, IDropHandler
{
    [SerializeField] private PlayerInventoryController playerInventoryController;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name);
        if (eventData.pointerDrag != null)
        {
            RectTransform droppedObject = eventData.pointerDrag.GetComponent<RectTransform>();
            InventoryItemButton inventoryItemButton = droppedObject.GetComponent<InventoryItemButton>();

            if (inventoryItemButton)
            {
                if (inventoryItemButton.oldParent != playerInventoryController.contentsTransform)
                {
                    droppedObject.SetParent(playerInventoryController.contentsTransform);
                    playerInventoryController.playerInventory.UnequipItem(inventoryItemButton.MyItem);
                    playerInventoryController.UpdateContents();
                }
                else
                {
                    Debug.Log("From Inventory. Ignore.");
                }
            }
        }
    }
}
