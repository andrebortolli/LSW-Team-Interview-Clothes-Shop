using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClothesShop.Trade;
using ClothesShop.UI.Menus.Prefabs;

namespace ClothesShop.UI
{
    public class UI_TradeButtonSelectItem : MonoBehaviour
    {
        private Button myButton;
        private InventoryItemButton menuUI_ItemPrefab;

        private void Awake()
        {
            myButton = GetComponent<Button>();
            menuUI_ItemPrefab = GetComponent<InventoryItemButton>();
        }

        private void OnEnable()
        {
            myButton.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            myButton.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            TradeController.Instance.playerSelectedItem = menuUI_ItemPrefab.MyItem;
        }
    }

}
