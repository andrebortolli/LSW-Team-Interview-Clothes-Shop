using ClothesShop.SO.Item;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransactionPanelItemPreviewImageListener : MonoBehaviour
{
    private Image myImage;

    private void Awake()
    {
        myImage = GetComponent<Image>();
    }

    public void OnSelectedItemChanged(Item _item, int _itemIndex)
    {
        //Check with item index, because if you do a null check on _item, it gives a null exception on the console. Go figure.
        if (_itemIndex >= 0)
        {
            myImage.sprite = _item?.previewSprite;
            myImage.enabled = true;
        }
    }
}
