using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;
using ClothesShop.Managers;

namespace ClothesShop.UI
{
    public class DialogController : MonoBehaviour
    {
        [Header("Setup")]
        public Transform dialogOptionsContent;

        [Header("Prefabs")]
        public GameObject dialogOptionButtonPrefab;

        bool selected = false;


        private void OnEnable()
        {
            SpeechPanelManager.Instance.onDialogSelectionMade.AddListener(OnDialogSelectionMethod);
        }

        private void OnDisable()
        {
            SpeechPanelManager.Instance.onDialogSelectionMade.RemoveListener(OnDialogSelectionMethod);
        }

        private void OnDialogSelectionMethod(SpeechPageDialogOption speechPageDialogOption)
        {
            selected = true;
            Debug.Log("Selected = " + selected.ToString());  
        }

        private void ClearOptions()
        {
            foreach (Transform child in dialogOptionsContent.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        public void InstantiateOptions(List<SpeechPageDialogOption> _dialogOptions)
        {
            ClearOptions();
            foreach (SpeechPageDialogOption option in _dialogOptions)
            {
                GameObject optionButton = Instantiate(dialogOptionButtonPrefab, dialogOptionsContent.transform);
                DialogOptionButton dialogOptionButton = optionButton.GetComponent<DialogOptionButton>();
                dialogOptionButton.Initialize(option);
            }
        }

        public void ShowDialog()
        {
            dialogOptionsContent.gameObject.SetActive(true);
        }
    }
}

