using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace ClothesShop.SO.Item
{
    /// <summary>
    /// This class represents an in-game item
    /// </summary>
    [Serializable]
    public class Item : ScriptableObject
    {
        public int id; //Item ID
        public string description; //Item description
        public int value; //Item buying value
        public int resellValue; //Item selling value
        public Sprite uiSprite; //Sprite shown in the UI
    }
}
