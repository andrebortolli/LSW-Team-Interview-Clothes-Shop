using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClothesShop.SO.Item;
using ClothesShop.SO.Inventory;
using TMPro;
using UnityEngine.UI;

namespace ClothesShop.UI.Menus
{
    public class MenuUI_PlayerInventory : MonoBehaviour
    {
        [Header("Player Data")]
        public Inventory playerInventory;

        [Header("Menu Objects")]
        public TextMeshProUGUI inventoryValue;
        public Transform contentsTransform;
        public GameObject itemMenuPrefab;

        private void OnEnable()
        {
            UpdateContents();
        }

        private void OnDisable()
        {
            ClearContents();
        }

        public void ClearContents()
        {
            foreach (Transform child in contentsTransform.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        public void UpdateContents()
        {
            ClearContents();
            foreach(Item item in playerInventory.Items)
            {
                GameObject itemMenuPrefabInstance = Instantiate(itemMenuPrefab, contentsTransform);
                itemMenuPrefabInstance.name = item.itemName;
                itemMenuPrefabInstance.GetComponent<Prefabs.MenuUI_ItemPrefab>().Initialize(item);
            }
            inventoryValue.text = string.Format("Value: {0:D4} / {1:D4}", playerInventory.GetMonetaryValue(false), playerInventory.GetMonetaryValue(true));
        }
    }
}

