using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClothesShop.Mechanics.Interaction
{
    [Serializable]
    public abstract class Interaction : ScriptableObject
    {
        public abstract void OnInteraction(GameObject _interactionSourceGameObject, GameObject _interactedGameObject);
    }
}
