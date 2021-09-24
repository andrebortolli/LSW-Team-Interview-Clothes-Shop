using ClothesShop.Mechanics.Interaction;
using ClothesShop.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UI_PlayerInteractionNameHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI myTextMeshProUGUI;

    public void UpdateUI(Interactable _interactable)
    {
        myTextMeshProUGUI.text = _interactable.gameObject.name;
    }

    public void Clear()
    {
        myTextMeshProUGUI.text = "";
    }
}
