using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogOptionButton : MonoBehaviour
{
    private Button myButton;
    public TMPro.TextMeshProUGUI myTextMeshProUGUI;
    private SpeechPageDialogOption mySpeechPageDialogOption;

    private void Awake()
    {
        myButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        myButton.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        myButton.onClick.RemoveListener(OnClick);
    }

    public void Initialize(SpeechPageDialogOption _speechPageDialogOption)
    {
        mySpeechPageDialogOption = _speechPageDialogOption;
        myTextMeshProUGUI.text = mySpeechPageDialogOption.optionName;
    }

    public void OnClick()
    {
        mySpeechPageDialogOption.onClickEvent.Raise();
    }
}
