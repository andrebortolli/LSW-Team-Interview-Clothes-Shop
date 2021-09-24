using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClothesShop.Shop.Portrait;

namespace ClothesShop.SO.Player.NPC
{
    [CreateAssetMenu(menuName = "Game/Players/New NPC")]
    public class NPCPlayer : Player
    {
        [Header("NPC Shop Portaits")]
        [SerializeField]
        public ShopPortraits npcShopPortraits;

        public override void BuyItem(Item.Item _itemToBuy)
        {
            inventory.Items.Add(_itemToBuy);
            wallet.SetValue(wallet.Value - _itemToBuy.resellValue);
            //Debug.Log(_itemToBuy.resellValue);
        }

        public override void SellItem(Item.Item _itemToSell)
        {
            inventory.Items.Remove(_itemToSell);
            wallet.SetValue(wallet.Value + _itemToSell.value);
            //Debug.Log(_itemToSell.value);
        }

        public override void SellItem(Item.Item _itemToSell, int _removeIndex)
        {
            Debug.Log("Item at " + _removeIndex + " is: " + inventory.Items[_removeIndex].itemName + " | Selected item: " + _itemToSell.itemName);
            if (inventory.Items[_removeIndex] == _itemToSell)
            {
                Debug.Log("Removed Exact Item");
                inventory.Items.RemoveAt(_removeIndex);
            }
            else
            {
                Debug.Log("Removed First Item");
                inventory.Items.Remove(_itemToSell);
            }
            wallet.SetValue(wallet.Value + _itemToSell.value);
        }
    }
}