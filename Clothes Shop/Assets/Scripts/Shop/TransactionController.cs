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
using ClothesShop.UI.Menus;

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

        public ShopController shopController;

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

        public IEnumerator ShopInteractionEnumerator(Player _player1, Player _player2, SpeechPage[] _pagesToDisplay)
        {
            yield return SpeechPanelManager.Instance.StartCoroutine(SpeechPanelManager.Instance.ShowSpeechPages(_pagesToDisplay));
            shopController.Initialize(_player1, _player2);
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
