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

        public void Initialize(Item _item)
        {
            myItem = _item;
            itemIcon.sprite = myItem.uiSprite;
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            oldParent = this.transform.parent;
            this.transform.parent = ParentCanvas.transform;
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
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
