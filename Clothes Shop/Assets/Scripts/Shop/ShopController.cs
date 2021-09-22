using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ClothesShop.SO.Item;
using ClothesShop.SO.Player.NPC;
using ClothesShop.Trade;
using ScriptableObjectExtensions.Variables;
using ClothesShop.SO.Player;
using ClothesShop.Players;

namespace ClothesShop.Shop
{
    [RequireComponent(typeof(PlayerData))]
    public class ShopController : MonoBehaviour
    {
        private PlayerData playerData;

        #region Events

        [Serializable]
        public class OnItemBuy : UnityEvent<Item> { }
        [Serializable]
        public class OnItemSell : UnityEvent<Item> { }

        [Header("Events")]
        public OnItemSell onItemBuy;

        public OnItemSell onItemSell;
        #endregion

        private void Awake()
        {
            playerData = GetComponent<PlayerData>();
        }
    }
}
