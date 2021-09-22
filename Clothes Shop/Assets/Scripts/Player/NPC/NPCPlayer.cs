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
    }
}