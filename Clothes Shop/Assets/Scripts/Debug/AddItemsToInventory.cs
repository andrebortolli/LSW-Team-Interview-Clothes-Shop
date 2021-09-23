using ClothesShop.SO.Inventory;
using ClothesShop.SO.Item;
using ClothesShop.SO.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItemsToInventory : MonoBehaviour
{
    public Inventory allItemsInventory;
    public Player[] playersToAddItems;

    public void AddItemToInventories(int _amountOfEach)
    {
        if (playersToAddItems.Length > 0)
        {
            foreach(Item item in allItemsInventory.Items)
            {
                for(int i = 0; i < _amountOfEach; i++)
                {
                    foreach(Player player in playersToAddItems)
                    {
                        player.inventory.AddItem(item);
                    }
                }
            }
            foreach(Player player in playersToAddItems)
            {
                player.wallet.Value += 10000;
            }
        }
    }
}
