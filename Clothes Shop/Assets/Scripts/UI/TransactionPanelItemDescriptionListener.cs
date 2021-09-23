using ClothesShop.SO.Item;
using ClothesShop.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransactionPanelItemDescriptionListener : TransactionOnSelectedItemChanged
{
    private AnimatedText myAnimatedText;

    private void Awake()
    {
        myAnimatedText = GetComponent<AnimatedText>();        
    }

    public override void OnSelectedItemChanged(Item _item, int _itemIndex)
    {
        if (_item != null)
        {
            myAnimatedText.AnimateText(_item.description);
        }
    }
}
