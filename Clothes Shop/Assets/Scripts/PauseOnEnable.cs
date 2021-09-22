using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClothesShop.Managers;

public class PauseOnEnable : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Instance.Pause();
    }

    private void OnDisable()
    {
        GameManager.Instance.Unpause();
    }
}
