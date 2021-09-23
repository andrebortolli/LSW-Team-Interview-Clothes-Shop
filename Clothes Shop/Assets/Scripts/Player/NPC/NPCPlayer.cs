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
            inventory.AddItem(_itemToBuy);
            wallet.SetValue(wallet.Value - _itemToBuy.resellValue);
            Debug.Log(_itemToBuy.resellValue);
        }

        public override void SellItem(Item.Item _itemToSell)
        {
            inventory.RemoveItem(_itemToSell);
            wallet.SetValue(wallet.Value + _itemToSell.value);
            Debug.Log(_itemToSell.value);
        }
    }
}