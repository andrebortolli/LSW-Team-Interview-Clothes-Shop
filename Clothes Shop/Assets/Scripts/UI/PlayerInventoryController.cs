using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClothesShop.SO.Item;
using ClothesShop.SO.Inventory;
using TMPro;
using UnityEngine.UI;

namespace ClothesShop.UI.Menus
{
    public class PlayerInventoryController : MonoBehaviour
    {
        [Header("Player Data")]
        public Inventory playerInventory;

        [Header("Menu Objects")]
        public TextMeshProUGUI inventoryValue;
        public Transform contentsTransform;
        public GameObject itemMenuPrefab;

        [Header("UI")]
        public Transform equipSlotsParentTransform;
        public List<ItemSlot> equipItemSlots;


        private void Awake()
        {
            equipItemSlots = new List<ItemSlot>(equipSlotsParentTransform.GetComponentsInChildren<ItemSlot>());
        }

        private void OnEnable()
        {
            UpdateContents();
        }

        private void OnDisable()
        {
            ClearContents();
        }

        //private void DestroyChildren(GameObject _gameObject)
        //{
        //    Transform gameObjectTransform = _gameObject.GetComponent<Transform>();
        //    foreach (Transform child in gameObjectTransform.transform)
        //    {
        //        GameObject.Destroy(child.gameObject);
        //    }
        //}

        private void DestroyChildren(Transform _transform)
        {
            foreach (Transform child in _transform.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        public void ClearContents()
        {
            DestroyChildren(contentsTransform);
            foreach (ItemSlot itemSlot in equipItemSlots)
            {
                DestroyChildren(itemSlot.transform);
            }
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

