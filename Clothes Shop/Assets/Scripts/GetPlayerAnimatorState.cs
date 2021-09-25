using ClothesShop.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GetPlayerAnimatorState : MonoBehaviour
{
    public PlayerController playerController;
    private Animator myAnimator;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        myAnimator.runtimeAnimatorController = playerController.GetAnimatorRAC();
    }
}
