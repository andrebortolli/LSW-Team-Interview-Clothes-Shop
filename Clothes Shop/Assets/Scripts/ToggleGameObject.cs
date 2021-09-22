using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClothesShop.Miscellaneous
{
    public class ToggleGameObject : MonoBehaviour
    {
        private bool gameObjectActiveState;
        public GameObject gameObjectToToggle;

        private void OnEnable()
        {
            gameObjectActiveState = gameObjectToToggle.activeSelf;
        }

        public void SetState(bool _State)
        {
            gameObjectToToggle.SetActive(_State);
        }

        public void Toggle()
        {
            gameObjectToToggle.SetActive(!gameObjectToToggle.activeSelf);
        }
    }
}

