using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClothesShop.Trade;
using ClothesShop.SO.Player;
using ClothesShop.Players;

namespace ClothesShop.Mechanics.Interaction
{
    [CreateAssetMenu(menuName = "Game/Interaction/New Shop Interaction")]
    public class ShopInteraction : Interaction
    {
        public SpeechPage[] pagesToDisplay;

        public override void OnInteraction(Managers.GameManager _gameManager, GameObject _interactionSourceGameObject, GameObject _interactedGameObject)
        {
            Player player1, player2;
            player1 = _interactionSourceGameObject.GetComponent<PlayerData>().data;
            player2 = _interactedGameObject.GetComponent<PlayerData>().data;
            if (player1 != null && player2 != null)
            {
                _gameManager.TradeController.StartCoroutine(_gameManager.TradeController.StartTrade(player1, player2, pagesToDisplay));
            }
        }
    }
}