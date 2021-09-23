using ScriptableObjectExtensions.Variables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ClothesShop.UI
{
    public class IntSOToTextMeshProUGUIText : MonoBehaviour
    {
        [Header("Prefix and Suffix")]
        [SerializeField] private string prefixString;
        [SerializeField] private string suffixString;
        private TextMeshProUGUI textMeshProUGUI;
        [Header("IntVariable")]
        [SerializeField] private IntVariable intVariable;

        private void Awake()
        {
            textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            textMeshProUGUI.text = string.Format("{0}{1}{2}", prefixString, intVariable.Value.ToString(), suffixString);
        }
    }
}