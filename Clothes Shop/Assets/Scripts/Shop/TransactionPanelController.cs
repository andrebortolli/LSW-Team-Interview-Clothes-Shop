using ClothesShop.SO.Item;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ClothesShop.Shop.Transaction;
using UnityEngine.Events;
using System;

namespace ClothesShop.UI.Menus
{
    public class TransactionPanelController : MonoBehaviour
    {

        public Item currentSelectedItem;

        [Serializable]
        public class OnCurrentSelectedItemChanged : UnityEvent<Item> { }

        [Header("Menu Objects")]
        public TextMeshProUGUI inventoryValue;
        public Transform contentsTransform;
        public GameObject itemMenuPrefab;

        [Header("Events")]
        public OnCurrentSelectedItemChanged onCurrentSelectedItemChanged;

        private void OnEnable()
        {
            UpdateContents();
        }
        private void OnDisable()
        {
            ClearContents();
        }

        public void BuyCurrentItem()
        {
            if (currentSelectedItem != null)
            {
                TransactionController.Instance.ProcessTransaction(TransactionController.Instance.CreateTransaction(currentSelectedItem));
            }
        }

        public void ClearContents()
        {
            currentSelectedItem = null;
            onCurrentSelectedItemChanged?.Invoke(currentSelectedItem);
            foreach (Transform child in contentsTransform.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        public void UpdateContents()
        {
            ClearContents();
            foreach (Item item in TransactionController.Instance.originPlayerData.inventory.Items)
            {
                GameObject itemMenuPrefabInstance = Instantiate(itemMenuPrefab, contentsTransform);
                itemMenuPrefabInstance.name = item.itemName;
                itemMenuPrefabInstance.GetComponent < Prefabs.TransactionPanelInventoryButton>().Initialize(this, item);
            }
            inventoryValue.text = string.Format("Value: {0:D4} / {1:D4}", TransactionController.Instance.originPlayerData.inventory.GetMonetaryValue(false), TransactionController.Instance.originPlayerData.inventory.GetMonetaryValue(true));
        }
    }
}

