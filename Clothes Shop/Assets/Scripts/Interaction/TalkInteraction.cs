using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClothesShop.Mechanics.Interaction
{

    [CreateAssetMenu(menuName = "Game/Interaction/New Text Dialog Interaction")]
    public class TalkInteraction : Interaction
    {
        public string textToDisplay;

        public override void OnInteraction(Managers.GameManager _gameManager, GameObject _interactionSourceGameObject, GameObject _interactedGameObject)
        {
            _gameManager.SpeechPanelManager.ShowPanel(textToDisplay);
        }
    }
}