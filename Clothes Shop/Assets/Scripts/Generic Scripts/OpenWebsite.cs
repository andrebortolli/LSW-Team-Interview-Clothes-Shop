using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClothesShop.Miscellaneous
{
    public class OpenWebsite : MonoBehaviour
    {
        public void Open(string _websiteUrl)
        {
            Application.OpenURL(_websiteUrl);
        }
    }
}
