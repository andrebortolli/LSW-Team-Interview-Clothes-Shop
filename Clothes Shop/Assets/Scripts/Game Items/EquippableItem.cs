using ClothesShop.SO.Item;
using ScriptableObjectExtensions.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClothesShop.SO.Item.Equippable
{
    public class EquippableItem : Item
    {
        public enum EquippableType
        {
            Hat,
            Shirt,
            Pants,
            Boots,
            Weapon
        }

        [Header("Equip")]
        public bool isEquipped;
        [SerializeField] public EquippableType equippableType;

        public GameEvent onEquip;
        public GameEvent onUnequip;
    }
}
