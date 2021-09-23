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

        [Serializable]
        public class OnCurrentSelectedItemChanged : UnityEvent<Item> { }

        public enum ShopChoice
        {
            Uninitialized,
            Buy,
            Sell
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

        public void Initialize(Player _pcPlayer, Player _npcPlayer)
        {
            pcPlayer = _pcPlayer;
            npcPlayer = _npcPlayer;
            Debug.Log("Shop: ShopChoice = " + shopChoice.ToString());
            switch(shopChoice)
            {
                case ShopChoice.Buy:
                    break;

                case ShopChoice.Sell:
                    break;
            }
            UpdateContents();
            shopPanel.SetActive(true);
        }

        public Shop.Transaction.ItemTransaction CreateOrder(ShopChoice _shopChoice, Item _item)
        {
            switch (shopChoice)
            {
                case ShopChoice.Buy:
                    return new ItemTransaction(npcPlayer, pcPlayer, _item);
                case ShopChoice.Sell:
                    return new ItemTransaction(pcPlayer, npcPlayer, _item);
                default:
                    return null;
            }
        }

        public void ProcessCurrentItem()
        {
            if (currentSelectedItem != null)
            {
                TransactionController.Instance.ProcessTransaction(CreateOrder(shopChoice, currentSelectedItem));
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
            foreach (Item item in _player.inventory.Items)
            {
                GameObject itemMenuPrefabInstance = Instantiate(itemMenuPrefab, contentsTransform);
                itemMenuPrefabInstance.name = item.itemName;
                itemMenuPrefabInstance.GetComponent<Prefabs.TransactionPanelInventoryButton>().Initialize(this, item);
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

