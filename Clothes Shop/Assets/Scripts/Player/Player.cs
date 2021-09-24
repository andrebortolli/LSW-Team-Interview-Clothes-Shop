using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClothesShop.SO.Item;
using ClothesShop.SO.Inventory;
using ScriptableObjectExtensions.Variables;

namespace ClothesShop.SO.Player
{
    [CreateAssetMenu(menuName = "Game/Players/New Player")]
    public class Player : ScriptableObject
    {
        public int id;
        public StringVariable playerName;
        public Inventory.Inventory inventory;
        public IntVariable wallet;

        public virtual void BuyItem(Item.Item _itemToBuy)
        {
            inventory.AddItem(_itemToBuy);
            wallet.SetValue(wallet.Value - _itemToBuy.value);
        }
        public virtual void SellItem(Item.Item _itemToSell)
        {
            inventory.RemoveItem(_itemToSell);
            wallet.SetValue(wallet.Value + _itemToSell.resellValue);
        }
        public virtual void SellItem(Item.Item _itemToSell, int _removeIndex)
        {
            Debug.Log("Item at " + _removeIndex + " is: " + inventory.Items[_removeIndex].itemName + " | Selected item: " + _itemToSell.itemName);
            if (inventory.Items[_removeIndex] == _itemToSell)
            {
                Debug.Log("Removed Exact Item");
                inventory.RemoveItemAt(_removeIndex);
            }
            else
            {
                Debug.Log("Removed First Item");
                inventory.RemoveItem(_itemToSell);
            }
            wallet.SetValue(wallet.Value + _itemToSell.resellValue);
        }
    }
}