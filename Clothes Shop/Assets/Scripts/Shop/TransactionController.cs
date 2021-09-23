using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ClothesShop.SO.Item;
using ClothesShop.SO.Player.NPC;
using ClothesShop.Trade;
using ScriptableObjectExtensions.Variables;
using ClothesShop.SO.Player;
using ClothesShop.Players;
using TMPro;
using ClothesShop.Managers;

namespace ClothesShop.Shop.Transaction
{

    public class TransactionController : MonoBehaviour
    {
        private static TransactionController sInstance = null;

        public static TransactionController Instance
        {
            get { return sInstance; }
            private set { }
        }

        public Player originPlayerData;
        public Player destinationPlayerData;

        public GameObject transactionPanel;

        #region Events

        [Serializable]
        public class OnTransactionProcessed : UnityEvent<Transaction.ItemTransaction> { }
        [Serializable]
        public class OnTransactionDeclined : UnityEvent<Transaction.ItemTransaction> { }
        [Serializable]
        public class OnTransactionAccepted : UnityEvent<Transaction.ItemTransaction> { }

        [Header("Events")]
        public OnTransactionProcessed onTransactionProcessed;
        public OnTransactionDeclined onTransactionDeclined;
        public OnTransactionAccepted onTransactionAccepted;
        #endregion

        public void ProcessTransaction(Transaction.ItemTransaction _transactionToProcess)
        {
            bool transactionAllowed = _transactionToProcess.Process();
            if (transactionAllowed)
            {
                onTransactionAccepted?.Invoke(_transactionToProcess);
            }
            else
            {
                onTransactionDeclined?.Invoke(_transactionToProcess);
            }
            onTransactionProcessed?.Invoke(_transactionToProcess);
        }

        public Transaction.ItemTransaction CreateTransaction(Item _item)
        {
            return new ItemTransaction(originPlayerData, destinationPlayerData, _item);
        }

        public IEnumerator ShopInteractionEnumerator(Player _player1, Player _player2, SpeechPage[] _pagesToDisplay)
        {
            //If buy Origin -> player2 (NPC) | Destination -> player1 (PC)
            //If sell Origin -> player1 (PC) | Destination -> player2 (NPC)
            originPlayerData = _player2;
            destinationPlayerData = _player1;
            yield return SpeechPanelManager.Instance.StartCoroutine(SpeechPanelManager.Instance.ShowSpeechPages(_pagesToDisplay));
            yield return null;
        }

        private void Awake()
        {
            if (sInstance == null)
            {
                sInstance = GetComponent<TransactionController>();
            }
            else
            {
                Destroy(gameObject);
            }
        }

    }
}
