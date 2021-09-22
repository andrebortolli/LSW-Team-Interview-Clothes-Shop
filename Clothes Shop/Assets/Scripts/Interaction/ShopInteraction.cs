using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClothesShop.Mechanics.Interaction
{
    [CreateAssetMenu(menuName = "Game/Interaction/New Shop Interaction")]
    public class ShopInteraction : Interaction
    {
        public SpeechPage[] pagesToDisplay;

        public override void OnInteraction(Managers.GameManager _gameManager, GameObject _interactionSourceGameObject, GameObject _interactedGameObject)
        {
            _gameManager.SpeechPanelManager.StartCoroutine(_gameManager.SpeechPanelManager.ShowSpeechPages(pagesToDisplay));
            //Show greeting message
            //Enable shop menu
        }
    }
}