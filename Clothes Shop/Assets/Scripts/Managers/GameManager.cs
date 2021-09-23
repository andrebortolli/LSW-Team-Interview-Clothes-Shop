using ClothesShop.Shop.Transaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ClothesShop.Managers
{
    public class GameManager : MonoBehaviour
    {
        //Singleton Struct
        private static GameManager sInstance = null;

        public static GameManager Instance
        {
            get { return sInstance; }
            private set { }
        }

        public SpeechPanelManager SpeechPanelManager { get => Managers.SpeechPanelManager.Instance; }
        public TransactionController TransactionController { get => TransactionController.Instance; }
        private bool gamePaused = false;
        public bool GamePaused { get => gamePaused; }



        [Serializable]
        public class OnGamePause : UnityEvent { }
        [Serializable]
        public class OnGameUnpause : UnityEvent { };

        [Header("Events")]
        public OnGamePause onGamePause;
        public OnGameUnpause onGameUnpause;

        public void Pause()
        {
            gamePaused = true;
            onGamePause?.Invoke();
        }

        public void Unpause()
        {
            gamePaused = false;
            onGameUnpause?.Invoke();
        }

        private void Awake()
        {
            if (sInstance == null)
            {
                sInstance = GetComponent<GameManager>();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
