using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClothesShop.Mechanics.Interaction
{
    [Serializable]
    public abstract class Interaction : ScriptableObject
    {
        public abstract void OnInteraction(Managers.GameManager _gameManager, GameObject _interactionSourceGameObject, GameObject _interactedGameObject);
    }
}
