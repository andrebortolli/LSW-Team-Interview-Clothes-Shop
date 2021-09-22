using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ClothesShop.UI
{
    public class EventSystemFocusObject : MonoBehaviour
    {
        public GameObject gameObjectToFocus;

        public void OnEnable()
        {
            FocusGameObject();
        }

        public void FocusGameObject()
        {
            EventSystem.current.firstSelectedGameObject = gameObjectToFocus;
        }
    }
}


