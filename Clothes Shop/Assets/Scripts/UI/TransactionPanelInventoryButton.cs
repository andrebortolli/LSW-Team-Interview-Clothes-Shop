using ClothesShop.SO.Item;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ClothesShop.UI.Menus.Prefabs
{
    public class TransactionPanelInventoryButton : MonoBehaviour
    {
        private Item myItem;
        private Button myButton;
        private int myInventoryItemIndex;
        private ShopController transactionPanelController;

        [Header("Prefab Fields")]
        public Image itemIcon;
        public TextMeshProUGUI itemName;
        public TextMeshProUGUI itemValue;
        public TextMeshProUGUI itemResellValue;

        private void Awake()
        {
            myButton = GetComponent<Button>();
        }

        private void OnEnable()
        {
            myButton.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            myButton.onClick.RemoveListener(OnClick);
        }

        public void Initialize(ShopController _tpcRef, Item _item, int _inventoryItemIndex)
        {
            transactionPanelController = _tpcRef;
            myItem = _item;
            myInventoryItemIndex = _inventoryItemIndex;
            itemIcon.sprite = myItem.uiSprite;
            itemName.text = myItem.name;
            itemValue.text = myItem.value.ToString();
            itemResellValue.text = myItem.resellValue.ToString();
        }

        private void OnClick()
        {
            if (transactionPanelController.currentSelectedItem != myItem)
            {
                transactionPanelController.currentSelectedItem = myItem;
                transactionPanelController.currentSelectedIndex = myInventoryItemIndex;
                transactionPanelController.onCurrentSelectedItemChanged?.Invoke(myItem);
            }
        }
    }
}

