using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClothesShop.SO.Item;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace ClothesShop.UI.Menus.Prefabs
{
    public class InventoryItemButton : DragAndDrop
    {
        private Item myItem;
        //private Button myButton;
        private int myInventoryItemIndex;
        private PlayerInventoryController playerInventoryController;
        public Transform oldParent;

        [Header("Prefab Fields")]
        public Image itemIcon;

        public Item MyItem { get => myItem; set => myItem = value; }
        public int MyInventoryItemIndex { get => myInventoryItemIndex; set => myInventoryItemIndex = value; }

        //private void Awake()
        //{

        //    myButton = GetComponent<Button>();
        //}

        private void OnEnable()
        {
            //myButton.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            //myButton.onClick.RemoveListener(OnClick);
        }

        public void Initialize(PlayerInventoryController _picRef, Item _item, int _inventoryItemIndex)
        {
            playerInventoryController = _picRef;
            MyItem = _item;
            MyInventoryItemIndex = _inventoryItemIndex;
            itemIcon.sprite = MyItem.uiSprite;
        }

        private void OnClick()
        {
            //playerInventoryController.onCurrentSelectedItemChanged?.Invoke(myItem, myInventoryItemIndex);
        }

        public void ReturnToOldParent()
        {
            MyRectTransform.SetParent(oldParent);
            Recenter();
            oldParent = null;
        }

        public void Recenter()
        {
            MyRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            MyRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            MyRectTransform.anchoredPosition = Vector2.zero;
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
            if (MyRectTransform.parent.GetComponent<ItemSlot>() || MyRectTransform.parent.GetComponent<InventoryDropHandler>())
            {
                Debug.Log("Is valid item slot.");
            }
            else
            {
                Debug.Log("Is not an Item Slot! Return!");
                ReturnToOldParent();

            }
        }

        public override void OnInitializePotentialDrag(PointerEventData eventData)
        {
            base.OnInitializePotentialDrag(eventData);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            playerInventoryController.onLastSelectedItemChange?.Invoke(myItem, myInventoryItemIndex);
            Debug.Log("OnPointerDown");
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            Debug.Log("OnPointerUp");
        }
    }
}
