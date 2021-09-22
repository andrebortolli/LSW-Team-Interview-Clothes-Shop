using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClothesShop.Shop.Portrait
{
    [Serializable]
    public class Portrait
    {
        public string portraitName;
        public enum PortaitMood
        {
            Neutral,
            Happy,
            Sad,
            Angry,
            Curious,
            Sus,
            Sly
        }

        public PortaitMood portaitMood;
        public Sprite portraitSprite;
    }
}