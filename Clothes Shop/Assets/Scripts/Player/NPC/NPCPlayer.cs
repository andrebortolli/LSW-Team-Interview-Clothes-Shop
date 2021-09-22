using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClothesShop.Shop.Portrait;

namespace ClothesShop.SO.Player.NPC
{
    [CreateAssetMenu(menuName = "Game/Shop/New NPC Portrait Data")]
    public class NPCPlayer : Player
    {
        [Header("NPC Shop Portaits")]
        [SerializeField]
        public ShopPortraits npcShopPortraits;
    }
}