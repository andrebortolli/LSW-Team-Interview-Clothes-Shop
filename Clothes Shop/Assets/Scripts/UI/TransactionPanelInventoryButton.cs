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

        public void Initialize(ShopController _tpcRef, Item _item)
        {
            transactionPanelController = _tpcRef;
            myItem = _item;
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
                transactionPanelController.onCurrentSelectedItemChanged?.Invoke(myItem);
            }
        }
    }
}

