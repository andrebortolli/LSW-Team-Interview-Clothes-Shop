using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace ClothesShop.UI
{
    public class YesNoPrompt : MonoBehaviour
    {
        private bool selection;
        private bool selected = false;
        public void SelectYes()
        {
            selection = true;
            selected = true;
        }

        public void SelectNo()
        {
            selection = false;
            selected = true;
        }

        private bool Selected()
        {
            return selected;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator WaitForSelection()
        {
            yield return new WaitUntil(Selected);
            selected = false;
            yield return selection;
        }
    }
}

