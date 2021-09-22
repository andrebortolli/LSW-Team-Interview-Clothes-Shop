using ClothesShop.Mechanics.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ClothesShop.Mechanics
{
    public class OnPlayerInteractableArea : MonoBehaviour
    {
        [Serializable]
        public class OnPlayerEnterInteractableArea : UnityEvent<Interactable> { }
        [Serializable]
        public class OnPlayerExitInteractableArea : UnityEvent<Interactable> { }

        public OnPlayerEnterInteractableArea onPlayerEnterInteractableArea;
        public OnPlayerExitInteractableArea onPlayerExitInteractableArea;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                onPlayerEnterInteractableArea?.Invoke(collision.gameObject.GetComponent<Interactable>());
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                onPlayerExitInteractableArea?.Invoke(collision.gameObject.GetComponent<Interactable>());
            }
        }
    }
}

