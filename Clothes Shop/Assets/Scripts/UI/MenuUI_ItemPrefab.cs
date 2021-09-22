using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClothesShop.SO.Item;
using TMPro;
using UnityEngine.UI;

namespace ClothesShop.UI.Menus.Prefabs
{
    public class MenuUI_ItemPrefab : MonoBehaviour
    {
        public Item myItem;
        [Header("Prefab Fields")]
        public Image itemIcon;
        public TextMeshProUGUI itemName;
        public TextMeshProUGUI itemValue;
        public TextMeshProUGUI itemResellValue;
    
        public void Initialize(Item _item)
        {
            myItem = _item;
            itemIcon.sprite = myItem.uiSprite;
            itemName.text = myItem.name;
            itemValue.text = myItem.value.ToString();
            itemResellValue.text = myItem.resellValue.ToString();
        }
    }
}
