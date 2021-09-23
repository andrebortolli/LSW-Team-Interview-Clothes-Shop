using ClothesShop.SO.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransactionPanelItemPreviewImageListener : TransactionOnSelectedItemChanged
{
    private Image myImage;

    private void Awake()
    {
        myImage = GetComponent<Image>();
    }

    public override void OnSelectedItemChanged(Item _item, int _itemIndex)
    {
        if (_item != null)
        {
            myImage.sprite = _item.previewSprite;
        }
    }
}
