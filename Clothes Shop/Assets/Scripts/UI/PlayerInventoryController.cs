using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClothesShop.SO.Item;
using ClothesShop.SO.Inventory;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

namespace ClothesShop.UI.Menus
{
    public class PlayerInventoryController : MonoBehaviour
    {
        [Header("Player Data")]
        public Inventory playerInventory;

        [Header("Menu Objects")]
        public TextMeshProUGUI inventoryValue;
        public RectTransform contentsTransform;
        public GameObject itemMenuPrefab;

        [Header("UI")]
        public Transform equipSlotsParentTransform;
        public List<ItemSlot> equipItemSlots;
        public AnimatedText descriptionText;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI valueText;
        public TextMeshProUGUI resellValueText;

        [Serializable] public class OnLastSelectedItemChange : UnityEvent<Item, int> { }

        public OnLastSelectedItemChange onLastSelectedItemChange;

        private Item lastSelectedItem;
        private int lastSelectedItemIndex;

        private void Awake()
        {
            equipItemSlots = new List<ItemSlot>(equipSlotsParentTransform.GetComponentsInChildren<ItemSlot>());
        }

        private void OnEnable()
        {
            onLastSelectedItemChange.AddListener(OnLastSelectedItemChangeMethod);
            UpdateContents();
        }

        private void OnDisable()
        {
            onLastSelectedItemChange.RemoveListener(OnLastSelectedItemChangeMethod);
        }

        public void OnLastSelectedItemChangeMethod(Item _item, int _itemIndex)
        {
            if (lastSelectedItem != _item)
            {
                lastSelectedItem = _item;
                lastSelectedItemIndex = _itemIndex;
                UpdateUI();
            }
        }

        //private void DestroyChildren(GameObject _gameObject)
        //{
        //    Transform gameObjectTransform = _gameObject.GetComponent<Transform>();
        //    foreach (Transform child in gameObjectTransform.transform)
        //    {
        //        GameObject.Destroy(child.gameObject);
        //    }
        //}

        void InitializeUI()
        {
            //Catch OnDestroy call
            if (this.gameObject.activeSelf)
            {
                nameText.text = "";
                valueText.text = "Buy value:";
                resellValueText.text = "Sell value:";
                descriptionText.AnimateText("Please select an item to view its description. \nYou can drag and drop items!");
            }
        }

        void UpdateUI()
        {
            nameText.text = lastSelectedItem.itemName;
            valueText.text = string.Format("Buy value: ${0}", lastSelectedItem.value);
            resellValueText.text = string.Format("Sell value: ${0}", lastSelectedItem.resellValue);
            descriptionText.AnimateText(lastSelectedItem.description);
        }

        private void DestroyChildren(Transform _transform)
        {
            foreach (Transform child in _transform.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        public void ShowInventoryUI()
        {
            this.gameObject.SetActive(true);
        }

        public void HideInventoryUI()
        {
            this.gameObject.SetActive(false);
        }

        public void ClearContents()
        {
            DestroyChildren(contentsTransform);
            foreach (ItemSlot itemSlot in equipItemSlots)
            {
                DestroyChildren(itemSlot.transform);
            }
            lastSelectedItem = null;
            lastSelectedItemIndex = -1;
            InitializeUI();
        }

        public void UpdateContents()
        {
            ClearContents();
            for (int i = 0; i < playerInventory.Items.Count; i++)
            {
                GameObject itemMenuPrefabInstance = Instantiate(itemMenuPrefab, contentsTransform);
                itemMenuPrefabInstance.name = playerInventory.Items[i].itemName;
                itemMenuPrefabInstance.GetComponent<Prefabs.InventoryItemButton>().Initialize(this, playerInventory.Items[i], i);
            }

            //Limit to the amount of equip slots available
            for (int i = 0; i < equipItemSlots.Count; i++)
            {
                if (i < playerInventory.EquippedItems.Count)
                {
                    GameObject itemMenuPrefabInstance = Instantiate(itemMenuPrefab, equipItemSlots[i].transform);
                    itemMenuPrefabInstance.name = playerInventory.EquippedItems[i].itemName;
                    itemMenuPrefabInstance.GetComponent<Prefabs.InventoryItemButton>().Initialize(this, playerInventory.EquippedItems[i], i);
                }
            }

            inventoryValue.text = string.Format("Value: {0:D4} / {1:D4}", playerInventory.GetMonetaryValue(false), playerInventory.GetMonetaryValue(true));
        }
    }
}

