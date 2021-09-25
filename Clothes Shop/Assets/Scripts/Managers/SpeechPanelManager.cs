using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClothesShop.UI;

using TMPro;
using UnityEngine.Events;
using System;

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

        [Header("Speech Panel Setup")]
        [SerializeField] private Animator darkerBackground;
        [SerializeField] private GameObject speechPanel;
        [SerializeField] private GameObject speakerNamePanel;
        [SerializeField] private TextMeshProUGUI speakerName;
        [SerializeField] private AnimatedText speechText;
        [SerializeField] private Image speakerPortrait;
        [SerializeField] private DialogController dialogController;
        private bool cancel = false;
        [Serializable] public class OnDialogSelectionMade : UnityEvent<SpeechPageDialogOption> { }

        [Header("Events")]
        public OnDialogSelectionMade onDialogSelectionMade;


        private void OnEnable()
        {
            onDialogSelectionMade.AddListener(OnDialogSelectionMethod);
            speechText.onWaitingForEndDialog.AddListener(OnWaitingForDialogEnd);
        }

        private void OnDisable()
        {
            onDialogSelectionMade.RemoveListener(OnDialogSelectionMethod);
            speechText.onWaitingForEndDialog.RemoveListener(OnWaitingForDialogEnd);
        }

        private SpeechPage currentSpeechPage;

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

        private void OnDialogSelectionMethod(SpeechPageDialogOption speechPageDialogOption)
        {
            Debug.Log(speechPageDialogOption.optionName + " was selected!");
            speechText.EndDialogWait();
        }

        private void OnWaitingForDialogEnd()
        {
            dialogController.ShowDialog();
        }

        private void Initialize(ref SpeechPage _speechPage)
        {
            currentSpeechPage = _speechPage;
            switch (_speechPage.speechStyle)
            {
                case SpeechPage.SpeechStyle.Simple:
                    //speechText.AnimateText(_speechPage.speechText);
                    speakerName.text = null;
                    speakerPortrait.sprite = null;

                    //Set object active states
                    speakerNamePanel.SetActive(false);
                    speechText.gameObject.SetActive(true);
                    speakerPortrait.gameObject.SetActive(false);
                    break;
                case SpeechPage.SpeechStyle.SimpleSpeakerName:
                    //speechText.AnimateText(_speechPage.speechText);
                    speakerName.text = _speechPage.speakerName;
                    speakerPortrait.sprite = null;

                    //Set object active states
                    speakerNamePanel.SetActive(true);
                    speechText.gameObject.SetActive(true);
                    speakerPortrait.gameObject.SetActive(false);
                    break;
                case SpeechPage.SpeechStyle.Full:
                    //speechText.AnimateText(_speechPage.speechText);
                    speakerName.text = _speechPage.speakerName;
                    speakerPortrait.sprite = _speechPage.speakerSprite;

                    //Set object active states
                    speakerNamePanel.SetActive(true);
                    speechText.gameObject.SetActive(true);
                    speakerPortrait.gameObject.SetActive(true);
                    break;
            }
            dialogController.InstantiateOptions(_speechPage.dialogOptions);
        }

        private IEnumerator ShowSpeechPage(SpeechPage _speechPage)
        {
            Initialize(ref _speechPage);
            speechPanel.SetActive(true);
            yield return speechText.AnimateText(_speechPage.speechText, _speechPage.isDialog);
        }

        public IEnumerator ShowSpeechPages(SpeechPage[] _speechPages)
        {
            darkerBackground.SetTrigger("Darken Background");
            foreach (SpeechPage page in _speechPages)
            {
                yield return StartCoroutine(ShowSpeechPage(page));
            }
            darkerBackground.SetTrigger("Lighten Background");
        }

        //private IEnumerator OnTextAnimationFinishedCoroutine()
        //{
        //    //CoroutineWithData cd = new CoroutineWithData(this, dialogController.WaitForSelection());
        //    //yield return cd.coroutine;
        //    //Debug.Log("result is " + (bool)cd.result);
        //    //speechPanel.SetActive(false);
        //}

        public void OnTextAnimationFinished()
        {
            speechPanel.SetActive(false);
        }

        private void Update()
        {

        }
    }
}