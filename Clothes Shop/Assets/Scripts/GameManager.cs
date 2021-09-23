using ClothesShop.Shop.Transaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
       

        public void Pause()
        {
            gamePaused = true;
        }

        public void Unpause()
        {
            gamePaused = false;
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
