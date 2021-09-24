using ClothesShop.SO.Inventory;
using ClothesShop.SO.Item;
using ClothesShop.UI.Menus.Prefabs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ItemSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private Inventory playerInventory;
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

                droppedObject.SetParent(this.transform);
                inventoryItemButton.Recenter();

                myEquippedItem = inventoryItemButton.MyItem;
                myEquippedItemIndex = inventoryItemButton.MyInventoryItemIndex;
                playerInventory.EquipItem(myEquippedItemIndex);
            }
        }
    }
}
