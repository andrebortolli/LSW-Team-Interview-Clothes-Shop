using ClothesShop.SO.Item;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ClothesShop.Shop.Transaction;
using UnityEngine.Events;
using System;
using ClothesShop.SO.Player;

namespace ClothesShop.UI.Menus
{
    public class ShopController : MonoBehaviour
    {

        public Item currentSelectedItem;
        public int currentSelectedIndex;

        [Serializable]
        public class OnCurrentSelectedItemChanged : UnityEvent<Item> { }

        public enum ShopChoice
        {
            Uninitialized,
            Buy,
            Sell,
            Nevermind
        }


        public ShopChoice shopChoice;

        [Header("Players")]
        public Player pcPlayer;
        public Player npcPlayer;

        [Header("Menu Objects")]
        public TextMeshProUGUI inventoryValue;
        public GameObject shopPanel;
        public Transform contentsTransform;
        public GameObject itemMenuPrefab;

        [Header("Events")]
        public OnCurrentSelectedItemChanged onCurrentSelectedItemChanged;

        public void SetShopChoiceToBuy()
        {
            shopChoice = ShopChoice.Buy;
        }

        public void SetShopChoiceToSell()
        {
            shopChoice = ShopChoice.Sell;
        }

        public void SetShopChoiceToNevermind()
        {
            pcPlayer = null;
            npcPlayer = null;
            shopChoice = ShopChoice.Nevermind;
        }

        public void SetShopChoiceToUninitialized()
        {
            pcPlayer = null;
            npcPlayer = null;
            shopChoice = ShopChoice.Uninitialized;
        }

        public void Initialize(Player _pcPlayer, Player _npcPlayer)
        {
            if (shopChoice == ShopChoice.Nevermind || shopChoice == ShopChoice.Uninitialized)
            {
                return;
            }
            pcPlayer = _pcPlayer;
            npcPlayer = _npcPlayer;
            UpdateContents();
            shopPanel.SetActive(true);
        }

        public Shop.Transaction.ItemTransaction CreateOrder(ShopChoice _shopChoice, Item _item, int _itemIndex)
        {
            switch (shopChoice)
            {
                case ShopChoice.Buy:
                    return new ItemTransaction(npcPlayer, pcPlayer, _item, _itemIndex);
                case ShopChoice.Sell:
                    return new ItemTransaction(pcPlayer, npcPlayer, _item, _itemIndex);
                default:
                    return null;
            }
        }

        public void ProcessCurrentItem()
        {
            if (currentSelectedItem != null)
            {
                TransactionController.Instance.ProcessTransaction(CreateOrder(shopChoice, currentSelectedItem, currentSelectedIndex));
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

        private void ListItems(Player _player)
        {
            for (int i = 0; i < _player.inventory.Items.Count; i++)
            {
                GameObject itemMenuPrefabInstance = Instantiate(itemMenuPrefab, contentsTransform);
                itemMenuPrefabInstance.name = _player.inventory.Items[i].itemName;
                itemMenuPrefabInstance.GetComponent<Prefabs.TransactionPanelInventoryButton>().Initialize(this, _player.inventory.Items[i], i);
            }
            inventoryValue.text = string.Format("Value: {0:D4} / {1:D4}", _player.inventory.GetMonetaryValue(false), _player.inventory.GetMonetaryValue(true));
        }

        public void UpdateContents()
        {
            ClearContents();
            switch (shopChoice)
            {
                case ShopChoice.Buy:
                    ListItems(npcPlayer);
                    break;
                case ShopChoice.Sell:
                    ListItems(pcPlayer);
                    break;
            }
        }
    }
}

