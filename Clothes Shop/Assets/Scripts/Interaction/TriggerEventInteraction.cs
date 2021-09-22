using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ClothesShop.Mechanics.Interaction
{
    [CreateAssetMenu(menuName = "Game/Interaction/New Trigger Event Interaction")]
    public class TriggerEventInteraction : Interaction
    {
        [Serializable] public class OnInteractionTrigger : UnityEvent { }
        public OnInteractionTrigger onInteractionTrigger;

        public override void OnInteraction(Managers.GameManager _gameManager, GameObject _interactionSourceGameObject, GameObject _interactedGameObject)
        {
            onInteractionTrigger?.Invoke();
        }
    }
}