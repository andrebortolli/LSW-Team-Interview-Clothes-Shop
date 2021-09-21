using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClothesShop.SO.Inventory;

namespace ClothesShop.SO.Player
{
    public class Player : ScriptableObject
    {
        public string id;
        public string playerName;
        public Inventory.Inventory inventory;
        public int wallet;
    }
}