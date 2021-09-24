using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using ScriptableObjectExtensions.Events;

namespace ClothesShop.SO.Item
{
    /// <summary>
    /// This class represents an in-game item
    /// </summary>
    [Serializable]
    public class Item : ScriptableObject
    {
        [Header("Information")]
        public int id; //Item ID
        public string itemName; //Item name
        public string description; //Item description
        public int value; //Item buying value
        public int resellValue; //Item selling value
        public Sprite uiSprite; //Sprite shown in the UI
        public Sprite previewSprite; //Sprite shown in the buying screen
        public AnimatorOverrideController animatorOverrideController; //AOC used for outfits;

        [Header("Use")]
        public bool singleUse;
        public GameEvent onUse;

        public enum EquippableType
        {
            NonEquippable,
            Body,
            Hair,
            Head,
            Weapon,
            Accessory
        }

        [Header("Equip")]
        [SerializeField] public EquippableType equippableType;
        [HideInInspector] public bool isEquipped;


        public GameEvent onEquip;
        public GameEvent onUnequip;
    }
}
