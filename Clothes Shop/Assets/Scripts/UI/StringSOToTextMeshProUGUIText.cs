using ScriptableObjectExtensions.Variables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ClothesShop.UI
{
    public class StringSOToTextMeshProUGUIText : MonoBehaviour
    {
        [Header("Prefix and Suffix")]
        [SerializeField] private string prefixString;
        [SerializeField] private string suffixString;
        private TextMeshProUGUI textMeshProUGUI;
        [Header("StringVariable")]
        [SerializeField] private StringVariable stringVariable;

        private void Awake()
        {
            textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            textMeshProUGUI.text = string.Format("{0}{1}{2}", prefixString, stringVariable.Value, suffixString);
        }
    }
}