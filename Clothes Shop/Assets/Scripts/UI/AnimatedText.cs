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
        bool isDialog = false;
        bool endDialog = false;
        Coroutine currentAnimationCoroutine;

        #region Events
        [Serializable] public class OnTextAnimationStarted : UnityEvent { }
        [Serializable] public class OnTextAnimationUpdate : UnityEvent { }
        [Serializable] public class OnTextAnimationPageFinished : UnityEvent { }
        [Serializable] public class OnTextAnimationNewPage : UnityEvent { }
        [Serializable] public class OnWaitingForEndDialog : UnityEvent { }
        [Serializable] public class OnEndDialog : UnityEvent { }
        [Serializable] public class OnTextAnimationFinished : UnityEvent { }

        [Header("Events")]
        public OnTextAnimationStarted onTextAnimationStarted;
        public OnTextAnimationUpdate onTextAnimationUpdate;
        public OnTextAnimationPageFinished onTextAnimationPageFinished;
        public OnTextAnimationNewPage onTextAnimationNewPage;
        public OnWaitingForEndDialog onWaitingForEndDialog;
        public OnEndDialog onEndDialog;
        public OnTextAnimationFinished onTextAnimationFinished;

        #endregion

        private void OnEnable()
        {
            onTextAnimationPageFinished.AddListener(OnPageFinished);
            onTextAnimationNewPage.AddListener(OnNewPageStarted);
        }

        private void OnDisable()
        {
            StopAllCoroutines();
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

        public void EndDialogWait()
        {
            endDialog = true;
        }

        private bool EndDialog()
        {
            return endDialog;
        }

        private void Update()
        {
            //Speed up the animation if the player holds the run button.
            if (Input.GetKey(Settings.GameSettings.RunKey))
            {
                currentWriteSpeed = writeSpeed / fastForwardSpeed;
            }
            else
            {
                if (Input.GetKeyUp(Settings.GameSettings.RunKey))
                {
                    currentWriteSpeed = writeSpeed;
                }
            }
        }

        ///// <summary>
        ///// This method will run the AnimateTextCoroutine on this GameObject, in a managed way that prevents more than
        ///// one coroutine executing at a time, thus preventing text corruption.
        ///// </summary>
        ///// <param name="_textToAnimate"></param>
        ///// <param name="_isDialog"></param>
        ///// <returns></returns>
        //public void AnimateText(string _textToAnimate, bool _isDialog = false)
        //{
        //    if (currentAnimationCoroutine != null)
        //    {
        //        StopCoroutine(currentAnimationCoroutine);
        //    }
        //    currentAnimationCoroutine = StartCoroutine(AnimateTextCoroutine(_textToAnimate, _isDialog));
        //}

        /// <summary>
        /// This method will run the AnimateTextCoroutine on this GameObject, in a managed way that prevents more than
        /// one coroutine executing at a time, thus preventing text corruption.
        /// </summary>
        /// <param name="_textToAnimate"></param>
        /// <param name="_isDialog"></param>
        /// <returns>Returns the started coroutine for yields</returns>
        public Coroutine AnimateText(string _textToAnimate, bool _isDialog = false)
        {
            if (currentAnimationCoroutine != null)
            {
                StopCoroutine(currentAnimationCoroutine);
            }
            currentAnimationCoroutine = StartCoroutine(AnimateTextCoroutine(_textToAnimate, _isDialog));
            return currentAnimationCoroutine;
        }

        /// <summary>
        /// This coroutine will separate the inputted string _textToAnimate into various pages, according to the
        /// page character limit and will animate the writing of it letter by letter into a TextMeshProUGUI object.
        /// If isDialog parameter is to be used, you must return EndDialog once a choice was made, so the coroutine can
        /// finish.
        /// </summary>
        /// <param name="_textToAnimate"></param>
        /// <param name="_isDialog"></param>
        /// <returns></returns>
        private IEnumerator AnimateTextCoroutine(string _textToAnimate, bool _isDialog = false)
        {
            //_textToAnimate is going to be a single SpeechPage file.
            //Not to mistake SpeechPages with pages. The both of them are not related
            //A SpeechPage can still be separated in various pages by this script
            //If isADialog is specified, the script will ignore the last interaction press
            //So the input can be used for something else
            onTextAnimationStarted?.Invoke();
            isDialog = _isDialog;
            endDialog = false;
            string[] pages = SeparatePages(_textToAnimate);
            for (int i = 0; i < pages.Length; i++)
            { //For each page, animate the page contents
                textMeshProUGUI.text = "";
                onTextAnimationNewPage?.Invoke();

                
                for (int j = 0; j < pages[i].Length; j++)
                { //For each character, type them until the end of page is reached.
                    isAnimating = true;
                    textMeshProUGUI.text += pages[i][j];
                    onTextAnimationUpdate?.Invoke();
                    yield return new WaitForSeconds(currentWriteSpeed);

                    if (j + 1 >= pages[i].Length)
                    { //End of page has reached
                        isAnimating = false;
                        onTextAnimationPageFinished?.Invoke();

                        //Trigger dialog events
                        if (_isDialog)
                        {
                            onWaitingForEndDialog?.Invoke();
                            yield return new WaitUntil(EndDialog);
                            onEndDialog?.Invoke();
                        }
                        else
                        {
                            yield return new WaitUntil(Next);
                        }
                    }
                }
            }
            //if (_isDialog)
            //{
            //    onWaitingForEndDialog?.Invoke();
            //    yield return new WaitUntil(EndDialog);
            //    onEndDialog?.Invoke();
            //}
            //else
            //{
            //    yield return new WaitUntil(Next);
            //}
            //yield return new WaitUntil(Next);

            //yield return new WaitUntil(Next);
            onTextAnimationFinished?.Invoke();
        }
    }
}
