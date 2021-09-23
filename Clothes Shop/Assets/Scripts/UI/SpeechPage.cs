using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClothesShop.SO.Player.NPC;
using ClothesShop.Shop.Portrait;
using UnityEditor;
using UnityEngine.Events;
using System;
using ScriptableObjectExtensions.Events;

[CreateAssetMenu(menuName = "Game/Speech/New Speech Page")]
public class SpeechPage : ScriptableObject
{
    public enum SpeechStyle
    {
        Simple,
        SimpleSpeakerName,
        Full
    }
    [Header("Speech Page Information")]
    public SpeechStyle speechStyle;
    public string speakerName;
    public string speechText;
    public Sprite speakerSprite;

    [Header("Dialog Settings")]
    public bool isDialog;
    public List<SpeechPageDialogOption> dialogOptions;

    void OnValidate()
    {

    }
}

[Serializable]
public class SpeechPageDialogOption
{
    public string optionName;
    public GameEvent onClickEvent;
}