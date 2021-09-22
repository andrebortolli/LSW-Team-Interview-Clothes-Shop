using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace ClothesShop.UI
{
    public class AnimatedText : MonoBehaviour
    {
        private TMPro.TextMeshProUGUI textMeshProUGUI;
        private string textToAnimate;
        [Header("Animation Settings")]
        [SerializeField] private float writeSpeed = 1.0f;
        [SerializeField] private float fastForwardSpeed = 4.0f;
        private float currentWriteSpeed;
        [Header("Page Settings")]
        [SerializeField] private int pageCharacterLimit = 64;
        bool pageLimitReached = false;
        bool isAnimating = false;

        #region Events
        [Serializable] public class OnTextAnimationStarted : UnityEvent { }
        [Serializable] public class OnTextAnimationPageFinished : UnityEvent { }
        [Serializable] public class OnTextAnimationNewPage : UnityEvent { }
        [Serializable] public class OnTextAnimationFinished : UnityEvent { }

        [Header("Events")]
        public OnTextAnimationStarted onTextAnimationStarted;
        public OnTextAnimationPageFinished onTextAnimationPageFinished;
        public OnTextAnimationNewPage onTextAnimationNewPage;
        public OnTextAnimationFinished onTextAnimationFinished;
        #endregion

        private void OnEnable()
        {
            onTextAnimationPageFinished.AddListener(OnPageFinished);
            onTextAnimationNewPage.AddListener(OnNewPageStarted);
        }

        private void OnDisable()
        {
            onTextAnimationPageFinished.RemoveListener(OnPageFinished);
            onTextAnimationNewPage.RemoveListener(OnNewPageStarted);
        }

        private void Awake()
        {
            textMeshProUGUI = GetComponent<TextMeshProUGUI>();
            currentWriteSpeed = writeSpeed;
        }

        private string[] SeparatePages(string _input)
        {
            return _input.Wrap(pageCharacterLimit);
        }

        private void OnPageFinished()
        {
            pageLimitReached = true;
        }

        private void OnNewPageStarted()
        {
            pageLimitReached = false;
        }

        private bool Next()
        {
            return Input.GetKeyDown(Settings.GameSettings.InteractionKey);
        }

        private void Update()
        {
            //Speed up the animation if the player holds the interaction button.
            if (Input.GetKeyDown(Settings.GameSettings.InteractionKey))
            {
                currentWriteSpeed = writeSpeed / fastForwardSpeed;
            }
            if (Input.GetKeyUp(Settings.GameSettings.InteractionKey))
            {
                currentWriteSpeed = writeSpeed;
            }
        }

        public IEnumerator AnimateText(string _textToAnimate)
        {
            onTextAnimationStarted?.Invoke();
            string[] pages = SeparatePages(_textToAnimate);
            for (int i = 0; i < pages.Length; i++)
            { //For each page, animate the page contents
                textMeshProUGUI.text = "";
                onTextAnimationNewPage?.Invoke();
                for (int j = 0; j < pages[i].Length; j++)
                {
                    isAnimating = true;
                    textMeshProUGUI.text += pages[i][j];
                    yield return new WaitForSeconds(currentWriteSpeed);

                    if (j + 1 >= pages[i].Length)
                    { //Last loop
                        isAnimating = false;
                        onTextAnimationPageFinished?.Invoke();
                        yield return new WaitUntil(Next);
                    }
                }
            }
            yield return new WaitUntil(Next);
            onTextAnimationFinished?.Invoke();
        }
    }
}
