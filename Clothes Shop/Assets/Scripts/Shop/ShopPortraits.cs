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


        /// <summary>
        /// Gets a random mood sprite according to the specified mood parameter. Returns null if none are found.
        /// </summary>
        /// <param name="_portaitMood"></param>
        /// <returns></returns>
        public Sprite GetRandomMoodSprite(Portrait.PortaitMood _portaitMood)
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
                    return moodMatchPortraits[0].portraitSprite;
                }
                else
                {

                    return moodMatchPortraits[UnityEngine.Random.Range(0, moodMatchPortraits.Count)].portraitSprite;
                }
            }
        }
    }
}