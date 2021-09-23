using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClothesShop.SO.Item;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace ClothesShop.UI.Menus.Prefabs
{
    public class InventoryItemPrefab : DragAndDrop
    {
        public Item myItem;
        [Header("Prefab Fields")]
        public Image itemIcon;
        Transform oldParent;

        public void ReturnToOldParent()
        {
            MyRectTransform.SetParent(oldParent);
            MyRectTransform.anchoredPosition = Vector2.zero;
            oldParent = null;
        }

        public void Initialize(Item _item)
        {
            myItem = _item;
            itemIcon.sprite = myItem.uiSprite;
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            oldParent = this.transform.parent;
            MyRectTransform.SetParent(ParentCanvas.transform);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            if (MyRectTransform.parent.GetComponent<ItemSlot>() == null)
            {
                Debug.Log("Is not an Item Slot! Return!");
                ReturnToOldParent();
            }
            else
            {
                Debug.Log("Is Item Slot.");
            }
        }

        public override void OnInitializePotentialDrag(PointerEventData eventData)
        {
            base.OnInitializePotentialDrag(eventData);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
        }
    }
}
