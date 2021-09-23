using ClothesShop.SO.Item;
using ClothesShop.SO.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClothesShop.Shop.Transaction
{
    public class ItemTransaction
    {
        private Player origin;
        private Player destination;
        private Item item;

        public ItemTransaction (Player _origin, Player _destination, Item _item)
        {
            Origin = _origin;
            Destination = _destination;
            Item = _item;
        }

        public bool Process()
        {
            //Check if the buying player have sufficient funds.
            bool buyerHasEnoughFunds = Destination.wallet.Value >= item.value;

            if (buyerHasEnoughFunds)
            {
                Origin.SellItem(Item);
                Destination.BuyItem(Item);
            }

            return buyerHasEnoughFunds;
        }

        public Player Origin { get => origin; set => origin = value; }
        public Player Destination { get => destination; set => destination = value; }
        public Item Item { get => item; set => item = value; }
    }

}