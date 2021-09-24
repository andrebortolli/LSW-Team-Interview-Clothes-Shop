﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ClothesShop.Managers;
using ClothesShop.SO.Player;
using ClothesShop.Mechanics.Interaction;
using ClothesShop.Settings;
using ScriptableObjectExtensions.Variables;
using ClothesShop.SO.Item;

namespace ClothesShop.Players
{
    [RequireComponent(typeof(PlayerData))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Player Settings")]
        private PlayerData playerData;
        [SerializeField] private FloatVariable walkingSpeed;
        [SerializeField] private FloatVariable runningSpeed;


        private Animator playerAnimator;
        private AnimatorOverrideController defaultAOC;
        private AnimatorOverrideController newAOC;
        private Rigidbody2D playerRigidbody;
        private Interactable interactableObject;

        #region Animator Parameters

        [Header("Animator Parameters")]
        public string horizontalMovementParameterName;
        public string verticalMovementParameterName;
        public string speedParameterName;

        #endregion

        #region Events
        [Serializable]
        public class OnInteracted : UnityEvent { }

        [Serializable]
        public class OnInteractableWithinReach : UnityEvent<Interactable> { }
        [Serializable]
        public class OnInteractableOutOfReach : UnityEvent<Interactable> { }

        public OnInteracted onInteracted;
        public OnInteractableWithinReach onInteractableWithinReach;
        public OnInteractableOutOfReach onInteractableOutOfReach;

        #endregion

        private void OnInteractableInReach(Interactable interactable)
        {
            interactableObject = interactable;
            Debug.Log("Interactable in reach: " + interactable.gameObject.name);
        }

        private void OnInteractableNotInReach(Interactable interactable)
        {
            interactableObject = null;
            Debug.Log("Interactable out of reach: " + interactable.gameObject.name);
        }

        private void Awake()
        {
            playerData = GetComponent<PlayerData>();
            playerAnimator = GetComponent<Animator>();
            playerRigidbody = GetComponent<Rigidbody2D>();
            TriggerEquippedItemsEvents();
        }

        public void TriggerEquippedItemsEvents()
        {
            foreach (Item item in playerData.data.inventory.EquippedItems)
            {
                item.onEquip.Raise();
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log(playerData.data.id + playerData.data.playerName.Value);
        }

        // Update is called once per frame
        void Update()
        {
            if (!GameManager.Instance.GamePaused)
            {
                if (Input.GetKeyDown(GameSettings.InteractionKey) && interactableObject != null)
                {
                    interactableObject.onInteraction?.Invoke(ClothesShop.Managers.GameManager.Instance, this.gameObject, interactableObject.gameObject);
                    onInteracted?.Invoke();
                }
            }
        }

        private void OnEnable()
        {
            onInteractableWithinReach.AddListener(OnInteractableInReach);
            onInteractableOutOfReach.AddListener(OnInteractableNotInReach);
        }

        private void OnDisable()
        {
            onInteractableWithinReach.RemoveListener(OnInteractableInReach);
            onInteractableOutOfReach.RemoveListener(OnInteractableNotInReach);
        }

        private void FixedUpdate()
        {
            if (!GameManager.Instance.GamePaused)
            {
                PlayerMovement();
            }
        }

        public AnimatorOverrideController FindAndReplaceAnimations(AnimatorOverrideController _baseAOC, AnimatorOverrideController _aocToApply, string _match)
        {
            if (_baseAOC.overridesCount != _aocToApply.overridesCount)
            {
                Debug.LogError("AOC override count doesn't match!");
                return _baseAOC;
            }

            AnimatorOverrideController workingAOC = new AnimatorOverrideController(_baseAOC);
            List<KeyValuePair<AnimationClip, AnimationClip>> workingAOCAnimPairList = new List<KeyValuePair<AnimationClip, AnimationClip>>();

            workingAOC.name = "FindAndReplace Custom";

            for(int i = 0; i < _baseAOC.animationClips.Length; i++)
            {
                Debug.Log("<color=brown>Current Animation Name:</color> " + _baseAOC.animationClips[i].name + " | " + _aocToApply.animationClips[i].name + " | " + _match);
                if (_baseAOC.animationClips[i].name == _aocToApply.animationClips[i].name && _aocToApply.animationClips[i].name.Contains(_match))
                {
                    Debug.Log("<color=green>Is equal and matches: </color>" + _aocToApply.animationClips[i].name);
                    workingAOCAnimPairList.Add(new KeyValuePair<AnimationClip, AnimationClip>(_baseAOC.animationClips[i], _aocToApply.animationClips[i]));
                }
                else
                {
                    Debug.Log("<color=red>Is not equal and/or doesn't match: </color>" + _aocToApply.animationClips[i].name);
                    workingAOCAnimPairList.Add(new KeyValuePair<AnimationClip, AnimationClip>(_baseAOC.animationClips[i], _baseAOC.animationClips[i]));
                }
            }

           
            Debug.Log("Applying Overrides");
            workingAOC.ApplyOverrides(workingAOCAnimPairList);

            return workingAOC;
        }

        public void ProcessAOCs()
        {
            Debug.Log("ProcessAOCs Initialized");
            AnimatorOverrideController currentAOC = new AnimatorOverrideController(playerAnimator.runtimeAnimatorController);

            if (playerData.data.inventory.EquippedItems.Count == 0)
            {

            }

            foreach (Item item in playerData.data.inventory.EquippedItems)
            {
                Debug.Log("Equipped Item Name: " + item.itemName);
                if (item.equippableType != Item.EquippableType.NonEquippable)
                {
                    Debug.Log("Is equippable");

                    switch (item.equippableType)
                    {
                        case Item.EquippableType.Body:
                            Debug.Log("Equippable Type: Body");
                            break;
                        case Item.EquippableType.Hair:
                            Debug.Log("Equippable Type: Hair");
                            //playerAnimator.runtimeAnimatorController = item.animatorOverrideController;
                            playerAnimator.runtimeAnimatorController = FindAndReplaceAnimations(currentAOC, item.animatorOverrideController, "_Hair");
                            break;
                        case Item.EquippableType.Head:
                            Debug.Log("Equippable Type: Head");
                            break;
                    }
                }
                else
                {
                    Debug.Log("Is not equippable.");
                }
            }
        }

        public void ProcessAOC(Item _item)
        {
            Debug.Log("Processing Item AOC: " + _item.itemName);
            AnimatorOverrideController currentAOC = new AnimatorOverrideController(playerAnimator.runtimeAnimatorController);
            if (_item.equippableType != Item.EquippableType.NonEquippable)
            {
                Debug.Log("Item is equippable. Type: <color=yellow>" + _item.equippableType.ToString() + "</color>");

                switch (_item.equippableType)
                {
                    case Item.EquippableType.Body:
                        playerAnimator.runtimeAnimatorController = FindAndReplaceAnimations(currentAOC, _item.animatorOverrideController, "_Body");
                        break;
                    case Item.EquippableType.Hair:
                        playerAnimator.runtimeAnimatorController = FindAndReplaceAnimations(currentAOC, _item.animatorOverrideController, "_Hair");
                        break;
                    case Item.EquippableType.Head:
                        playerAnimator.runtimeAnimatorController = FindAndReplaceAnimations(currentAOC, _item.animatorOverrideController, "_Head");
                        break;
                }
            }
            else
            {
                Debug.Log("Item is not equippable.");
            }
        }


        /// <summary>
        /// Returns a unitary vector in a 4-way movement style, depending on the user input. Does not allow more than one key to be pressed at the same time.
        /// </summary>
        /// <returns></returns>
        private Vector2 Get4WayPlayerMovement()
        {
            Vector2 returnValue;
            float horizontalInputValue = Input.GetAxisRaw("Horizontal");
            float verticalInputValue = Input.GetAxisRaw("Vertical");

            //The following code will make it so that only 4-axis movements are valid.
            if (horizontalInputValue > 0)
            {
                if (verticalInputValue == 0)
                {
                    returnValue = Vector2.right;
                }
                else
                {
                    returnValue = Vector2.zero;
                }
            }
            else if (horizontalInputValue < 0)
            {
                if (verticalInputValue == 0)
                {
                    returnValue = Vector2.left;
                }
                else
                {
                    returnValue = Vector2.zero;
                }
            }
            else if (verticalInputValue > 0)
            {
                if (horizontalInputValue == 0)
                {
                    returnValue = Vector2.up;
                }
                else
                {
                    returnValue = Vector2.zero;
                }
            }
            else if (verticalInputValue < 0)
            {
                if (horizontalInputValue == 0)
                {
                    returnValue = Vector2.down;
                }
                else
                {
                    returnValue = Vector2.zero;
                }
            }
            else
            {
                returnValue = Vector2.zero;
            }
            return returnValue;
        }

        private void PlayerMovement()
        {
            Vector2 movementAxesValues = Get4WayPlayerMovement();

            //Sets the player's rigidbody's velocity
            Vector2 timeFixedMovementValues = new Vector2(movementAxesValues.x, movementAxesValues.y) * Time.fixedDeltaTime;

            if (Input.GetKey(GameSettings.RunKey))
            {
                playerRigidbody.MovePosition(playerRigidbody.position + timeFixedMovementValues * runningSpeed.Value);
                playerAnimator.speed = Mathf.Sqrt(runningSpeed.Value) / 2;
            }
            else
            {
                playerRigidbody.MovePosition(playerRigidbody.position + timeFixedMovementValues * walkingSpeed.Value);
                playerAnimator.speed = Mathf.Sqrt(walkingSpeed.Value) / 2;
            }

            //Update animator parameters
            playerAnimator.SetFloat(horizontalMovementParameterName, movementAxesValues.x);
            playerAnimator.SetFloat(verticalMovementParameterName, movementAxesValues.y);
            playerAnimator.SetFloat(speedParameterName, movementAxesValues.sqrMagnitude);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Interactable")
            {
                onInteractableWithinReach?.Invoke(collision.gameObject.GetComponent<Interactable>());
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Interactable")
            {
                onInteractableOutOfReach?.Invoke(collision.gameObject.GetComponent<Interactable>());
            }
        }
    }
}
