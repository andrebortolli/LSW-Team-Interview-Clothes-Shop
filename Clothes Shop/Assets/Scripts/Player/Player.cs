using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClothesShop.SO.Inventory;
using ScriptableObjectExtensions.Variables;

namespace ClothesShop.SO.Player
{
    public class Player : ScriptableObject
    {
        public int id;
        public StringVariable playerName;
        public Inventory.Inventory inventory;
        public IntVariable wallet;
    }
}