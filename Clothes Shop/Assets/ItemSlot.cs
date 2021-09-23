using ClothesShop.UI.Menus.Prefabs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ItemSlot : MonoBehaviour, IDropHandler
{
    private RectTransform myRectTransform;

    public RectTransform MyRectTransform { get => myRectTransform; set => myRectTransform = value; }

    private InventoryItemPrefab inventoryItemPrefab;


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
                inventoryItemPrefab = droppedObject.GetComponent<InventoryItemPrefab>();

                droppedObject.SetParent(this.transform);
                droppedObject.anchorMin = new Vector2(0.5f, 0.5f);
                droppedObject.anchorMax = new Vector2(0.5f, 0.5f);
                droppedObject.anchoredPosition = Vector2.zero;
            }
        }
    }
}
