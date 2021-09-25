using ClothesShop.SO.Inventory;
using ClothesShop.SO.Item;
using ClothesShop.UI.Menus;
using ClothesShop.UI.Menus.Prefabs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ItemSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private PlayerInventoryController playerInventoryController;
    private RectTransform myRectTransform;

    private InventoryItemButton inventoryItemButton;
    private Item myEquippedItem;
    private int myEquippedItemIndex;

    public RectTransform MyRectTransform { get => myRectTransform; set => myRectTransform = value; }
    public Item MyEquippedItem { get => myEquippedItem; set => myEquippedItem = value; }

    void Awake()
    {
        MyRectTransform = GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (myRectTransform.childCount == 0)
        {
            if (eventData.pointerDrag != null)
            {
                RectTransform droppedObject = eventData.pointerDrag.GetComponent<RectTransform>();
                inventoryItemButton = droppedObject.GetComponent<InventoryItemButton>();

                if (inventoryItemButton)
                {

                    if (inventoryItemButton.oldParent != this.transform && inventoryItemButton.oldParent.GetComponent<ItemSlot>() == null)
                    {
                        droppedObject.SetParent(this.transform);
                        inventoryItemButton.Recenter();

                        myEquippedItem = inventoryItemButton.MyItem;
                        myEquippedItemIndex = inventoryItemButton.MyInventoryItemIndex;
                        playerInventoryController.playerInventory.EquipItem(myEquippedItemIndex);
                        playerInventoryController.UpdateContents();
                    }
                    else
                    {
                        Debug.Log("Returned to same parent or another ItemSlot");
                    }
                }
            }
        }
    }
}
