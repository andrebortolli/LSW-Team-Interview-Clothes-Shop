using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClothesShop.Shop.Portrait
{
    [Serializable]
    public class ShopPortraits
    {
        public List<Portrait> npcShopPortraits;

        public Portrait GetPortraitSpriteByName(string _portraitName)
        {
            Portrait returnPortrait = null;
            foreach (Portrait portrait in npcShopPortraits)
            {
                if(portrait.portraitName == _portraitName)
                {
                    return portrait;
                }
            }
            return returnPortrait;
        }

        public Portrait GetPortraitSpriteByIndex(int _index)
        {
            if (_index + 1 <= npcShopPortraits.Count)
            {
                return npcShopPortraits[_index];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets a random mood sprite according to the specified mood parameter. Returns null if none are found.
        /// </summary>
        /// <param name="_portaitMood"></param>
        /// <returns></returns>
        public Portrait GetRandomMoodSprite(Portrait.PortaitMood _portaitMood)
        {
            List<Portrait> moodMatchPortraits = new List<Portrait>();
            foreach (Portrait portrait in npcShopPortraits)
            {
                if (portrait.portaitMood == _portaitMood)
                {
                    moodMatchPortraits.Add(portrait);
                }
            }
            if (moodMatchPortraits.Count == 0)
            {
                return null;
            }
            else
            {
                if (moodMatchPortraits.Count == 1)
                {
                    return moodMatchPortraits[0];
                }
                else
                {

                    return moodMatchPortraits[UnityEngine.Random.Range(0, moodMatchPortraits.Count)];
                }
            }
        }
    }
}