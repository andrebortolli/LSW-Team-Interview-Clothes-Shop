using System;
using System.Collections;
using System.Collections.Generic;
using ClothesShop.SO.Item;
using UnityEngine;

namespace ClothesShop.SO.Inventory
{
    [Serializable]
    [CreateAssetMenu(menuName = "Game/New Inventory")]
    public class Inventory : ScriptableObject
    {
         //SerializeField for the inventory to be visible on Unity's Editor
        [SerializeField] private List<Item.Item> items;

        [SerializeField] private List<Item.Item> equippedItems;
        public List<Item.Item> Items { get => items; set => items = value; }
        public List<Item.Item> EquippedItems { get => equippedItems; set => equippedItems = value; }

        #region Inventory Management Methods

        public void EquipItem(int _itemIndexOnInventory)
        {
            if (Items.Count > 0 && _itemIndexOnInventory <= Items.Count)
            {
                EquippedItems.Add(Items[_itemIndexOnInventory]);
                Items.RemoveAt(_itemIndexOnInventory);
            }
        }

        public void UnequipItem(Item.Item _item)
        {
            if(EquippedItems.Contains(_item))
            {
                Items.Add(_item);
                EquippedItems.Remove(_item);
            }
        }
        #endregion

        #region Inventory Information Methods

        /// <summary>
        /// Returns a list of all items' ids, names, buy values and sell values
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string returnValue = "";
            foreach(Item.Item item in Items)
            {
                returnValue += string.Format("{0} - {1} - {2} - {3}", item.id, item.name, item.value, item.resellValue);
            }
            return returnValue;
        }

        /// <summary>
        /// Gets the inventory monetary value for all items present. Can get either the buy or the sell values,
        /// according to the parameter. By default the sell value is selected.
        /// </summary>
        /// <returns>Returns an integer value representing the sum of all the items' values</returns>
        /// <param name="_getResellValue"></param>
        public int GetMonetaryValue(bool _getResellValue = true)
        {
            int returnValue = 0;
            if (Items.Count > 0)
            {
                //Checking for the parameter here is more optimized than checking at every loop.
                if (_getResellValue)
                {
                    foreach (Item.Item item in Items)
                    {
                        returnValue += item.resellValue;
                    }
                }
                else
                {
                    foreach (Item.Item item in Items)
                    {
                        returnValue += item.value;
                    }
                }
                return returnValue;
            }
            else
            {
                return returnValue;
            }
        }
        #endregion
    }
}
