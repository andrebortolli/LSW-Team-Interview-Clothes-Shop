using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace ClothesShop.UI
{
    public class DialogController : MonoBehaviour
    {
        [Header("Prefabs")]
        public GameObject dialogOptionButtonPrefab;

        private bool selected = false;

        private void ClearOptions()
        {
            foreach (Transform child in this.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        public void InstantiateOptions(List<SpeechPageDialogOption> _dialogOptions)
        {
            ClearOptions();
            foreach (SpeechPageDialogOption option in _dialogOptions)
            {
                GameObject optionButton = Instantiate(dialogOptionButtonPrefab, this.transform);
                DialogOptionButton dialogOptionButton = optionButton.GetComponent<DialogOptionButton>();
                dialogOptionButton.Initialize(option);
            }
        }

        private bool Selected()
        {
            return selected;
        }

        public void Select()
        {
            selected = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator WaitForSelection()
        {
            yield return new WaitUntil(Selected);
            selected = false;
            yield return null;
        }
    }
}

