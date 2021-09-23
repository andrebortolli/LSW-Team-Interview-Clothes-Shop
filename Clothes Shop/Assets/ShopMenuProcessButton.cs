using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ClothesShop.UI.Menus;
using ClothesShop.SO.Item;
using ScriptableObjectExtensions.Variables;
using UnityEngine.UI;

public abstract class OnCurrentItemSelectionChangedListener: MonoBehaviour
{
    public ShopController shopController;

    public virtual void OnEnable()
    {
        shopController.onCurrentSelectedItemChanged.AddListener(OnCurrentItemSelectionChanged);
    }

    public virtual void OnDisable()
    {
        shopController.onCurrentSelectedItemChanged.RemoveListener(OnCurrentItemSelectionChanged);
    }

    public abstract void OnCurrentItemSelectionChanged(Item _item, int _itemIndex);

}

public class ShopMenuProcessButton : OnCurrentItemSelectionChangedListener
{
    public TextMeshProUGUI textMeshProUGUI;
    public Button myButton;
    public IntVariable playerWallet;

    public override void OnCurrentItemSelectionChanged(Item _item, int _itemIndex)
    {
        if (_item != null)
        {
            if (shopController.shopChoice == ShopController.ShopChoice.Buy)
            {
                myButton.interactable = playerWallet.Value >= _item.value;
            }
            else
            {
                myButton.interactable = true;
            }
        }
        else
        {
            myButton.interactable = false;
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        if (shopController.shopChoice != ShopController.ShopChoice.Uninitialized || shopController.shopChoice != ShopController.ShopChoice.Nevermind)
        {
            textMeshProUGUI.text = shopController.shopChoice.ToString();
        }
    }
}
