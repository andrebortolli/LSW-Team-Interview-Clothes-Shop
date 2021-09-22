using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClothesShop.SO.Player.NPC;
using ClothesShop.Shop.Portrait;
using UnityEditor;

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
    public bool isQuestion;

    void OnValidate()
    {

    }
}