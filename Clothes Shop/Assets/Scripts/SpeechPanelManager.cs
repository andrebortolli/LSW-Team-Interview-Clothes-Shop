using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClothesShop.UI;

namespace ClothesShop.Managers
{
    public class SpeechPanelManager : MonoBehaviour
    {
        //Singleton Struct
        private static SpeechPanelManager sInstance = null;

        public static SpeechPanelManager Instance
        {
            get { return sInstance; }
            private set { }
        }

        public GameObject speechPanel;
        public AnimatedText animatedText;

        private void Awake()
        {
            if (sInstance == null)
            {
                sInstance = GetComponent<SpeechPanelManager>();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void ShowPanel(string _stringToDisplay)
        {
            speechPanel.SetActive(true);
            animatedText.StartCoroutine(animatedText.AnimateText(_stringToDisplay));
        }
        
        public void HidePanel()
        {
            animatedText.StopAllCoroutines();
            speechPanel.SetActive(false);
        }

        private void Update()
        {

        }
    }
}