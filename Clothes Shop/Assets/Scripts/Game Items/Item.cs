using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace ClothesShop.SO.Item
{
    [Serializable]
    public class Item : ScriptableObject
    {
        [SerializeField]
        public int id; //Item ID
        [SerializeField]
        public string description; //Item description
        [SerializeField]
        public int price; //Item buying price
        [SerializeField]
        public int resellPrice; //Item selling price
        [SerializeField]
        public Sprite uiSprite; //Sprite shown in the UI
    }
}
