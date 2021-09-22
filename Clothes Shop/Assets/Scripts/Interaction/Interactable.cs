using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ClothesShop.Mechanics.Interaction
{
    public class Interactable : MonoBehaviour
    {
        [Serializable]
        public class OnInteraction : UnityEvent<GameObject, GameObject> { }

        public OnInteraction onInteraction;
    }
}
