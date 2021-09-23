using ClothesShop.SO.Item;
using UnityEngine;

public abstract class TransactionOnSelectedItemChanged : MonoBehaviour
{
    public abstract void OnSelectedItemChanged(Item _item);
}