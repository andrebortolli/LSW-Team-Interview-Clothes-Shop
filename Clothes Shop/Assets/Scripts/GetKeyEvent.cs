using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ClothesShop.Settings;
using ClothesShop.Managers;

namespace ClothesShop.Events
{
    public class GetKeyEvent : MonoBehaviour
    {
        [Serializable] public class OnKeyDown : UnityEvent { }
        [Serializable] public class OnKeyUp : UnityEvent { }

        [Header("Settings")]
        [SerializeField] private KeyCode keyCode;
        [SerializeField] private bool ignoreIfGameIsPaused;

        [Header("Events")]
        public OnKeyDown onKeyDown;
        public OnKeyUp onKeyUp;

        private void ScanKeys()
        {
            if (Input.GetKeyDown(keyCode))
            {
                onKeyDown?.Invoke();
            }
            if (Input.GetKeyUp(keyCode))
            {
                onKeyUp?.Invoke();
            }
        }

        private void Update()
        {
            if (ignoreIfGameIsPaused == false)
            {
                ScanKeys();
            }
            else
            {
                if (!GameManager.Instance.GamePaused)
                {
                    ScanKeys();
                }
            }
        }
    }
}

