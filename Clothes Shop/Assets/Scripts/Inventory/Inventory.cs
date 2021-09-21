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
        [SerializeField] //SerializeField for the inventory to be visible on Unity's Editor
        private List<Item.Item> items;
        public List<Item.Item> Items { get => items; set => items = value; }

        #region Inventory Management Methods

        /// <summary>
        /// Removes an item at the specified index.
        /// </summary>
        /// <param name="_index"></param>
        public void RemoveItemAt(int _index)
        {
            Items.RemoveAt(_index);
        }

        /// <summary>
        /// Adds a specified item to the inventory.
        /// </summary>
        /// <param name="_itemToAdd"></param>
        public void AddItem(Item.Item _itemToAdd)
        {
            Items.Add(_itemToAdd);
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
