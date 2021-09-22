using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ClothesShop.SO.Item;
using ClothesShop.SO.Player.NPC;
using ScriptableObjectExtensions.Variables;

namespace ClothesShop.Shop
{
    public class ShopController : MonoBehaviour
    {
        [Header("Shopkeeper NPC Data")]
        public NPCPlayer shopkeeperNPCPlayerData;

        #region Events

        [Serializable]
        public class OnItemBuy : UnityEvent<Item> { }
        [Serializable]
        public class OnItemSell : UnityEvent<Item> { }

        [Header("Events")]
        public OnItemSell onItemBuy;

        public OnItemSell onItemSell;
        #endregion
    }
}
