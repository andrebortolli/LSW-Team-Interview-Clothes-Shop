using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClothesShop.SO.Item;
using ClothesShop.SO.Player;
using ClothesShop.SO.Inventory;
using ClothesShop.Managers;
using TMPro;

namespace ClothesShop.Trade
{
    public class TradeController : MonoBehaviour
    {
        //Singleton Struct
        private static TradeController sInstance = null;

        public static TradeController Instance
        {
            get { return sInstance; }
            private set { }
        }

        [Header("UI")]
        [SerializeField] private GameObject tradeMenu;
        [SerializeField] private TextMeshProUGUI playerInventoryTMP;
        [SerializeField] private TextMeshProUGUI npcInventoryTMP;


        [Header("Trade Settings")]
        public Item playerSelectedItem;



        private void Awake()
        {
            if (sInstance == null)
            {
                sInstance = GetComponent<TradeController>();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public IEnumerator StartTrade(Player _player1, Player _player2, SpeechPage[] _pagesToDisplay)
        {
            yield return SpeechPanelManager.Instance.StartCoroutine(SpeechPanelManager.Instance.ShowSpeechPages(_pagesToDisplay));
            playerInventoryTMP.text = string.Format("{0}'s Inventory", _player1.playerName.Value);
            npcInventoryTMP.text = string.Format("{0}'s Inventory", _player2.playerName.Value);
            tradeMenu.SetActive(true);
            yield return null;
        }

        public void Trade()
        {

        }

        public void ExitTrade()
        {
            playerSelectedItem = null;
        }
    }
}
