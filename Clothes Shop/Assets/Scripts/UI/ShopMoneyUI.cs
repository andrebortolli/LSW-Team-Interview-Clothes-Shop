using ScriptableObjectExtensions.Variables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ClothesShop.UI
{
    public class ShopMoneyUI : MonoBehaviour
    {
        [Header("Variables")]
        [SerializeField] private StringVariable playerName;
        [SerializeField] private IntVariable wallet;
        private TextMeshProUGUI textMeshProUGUI;

        private void Awake()
        {
            textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            if (playerName != null && wallet != null)
            {
                textMeshProUGUI.text = string.Format("{0}: ${1}", playerName.Value, wallet.Value.ToString());
            }
        }
    }
}