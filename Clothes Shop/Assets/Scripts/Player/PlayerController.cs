using System;
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
        private AnimatorOverrideController aoc;
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
            aoc = new AnimatorOverrideController(playerAnimator.runtimeAnimatorController);
            playerAnimator.runtimeAnimatorController = aoc;
            TriggerEquippedItemsEvents();
        }

        public void TriggerEquippedItemsEvents()
        {
            bool raisedClothItemEvent = false;
            foreach (Item item in playerData.data.inventory.EquippedItems)
            {
                //Assumes that only equippable items will be in the equippeditems list.
                if (item.equippableType == Item.EquippableType.Accessory)
                {
                    item.onEquip.Raise();
                }
                else
                {
                    if (raisedClothItemEvent == false)
                    {
                        item.onEquip.Raise();
                        raisedClothItemEvent = true;
                    }
                }
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            //Debug.Log(playerData.data.id + playerData.data.playerName.Value);
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

        public List<KeyValuePair<AnimationClip, AnimationClip>> FindAndReplaceAnimations(ref List<KeyValuePair<AnimationClip, AnimationClip>> _baseAOCOverrides, ref List<KeyValuePair<AnimationClip, AnimationClip>> _aocOverridesToApply, string _match)
        {
            if (_baseAOCOverrides.Count != _aocOverridesToApply.Count)
            {
                Debug.LogError("AOC override count doesn't match! Count: " + _baseAOCOverrides.Count);
                return _baseAOCOverrides;
            }

            List<KeyValuePair<AnimationClip, AnimationClip>> returnOverrides = new List<KeyValuePair<AnimationClip, AnimationClip>>();


            //Todo: null handling
            for (int i = 0; i < _baseAOCOverrides.Count; i++)
            {

                if (_baseAOCOverrides[i].Key.name == _aocOverridesToApply[i].Key.name && _baseAOCOverrides[i].Key.name.Contains(_match))
                {
                    //Debug.Log("<color=green>Is equal and matches: </color>" + _baseAOCOverrides[i].Key.name);
                    returnOverrides.Add(new KeyValuePair<AnimationClip, AnimationClip>(_baseAOCOverrides[i].Key, _aocOverridesToApply[i].Value));
                }
                else
                {
                    //Debug.Log("<color=red>Is not equal and/or doesn't match: </color>" + _baseAOCOverrides[i].Key.name);
                    returnOverrides.Add(_baseAOCOverrides[i]);
                }
            }

            return returnOverrides;
        }

        public void ProcessAllEquippedItemsAOCs()
        {
            List<Item> aocUpdatableItems = new List<Item>();
            foreach (Item item in playerData.data.inventory.EquippedItems)
            {
                if (item.equippableType != Item.EquippableType.NonEquippable && item.equippableType != Item.EquippableType.Accessory)
                {
                    aocUpdatableItems.Add(item);
                }
            }
            Debug.Log("Updatable Items: " + aocUpdatableItems.Count);

            NullifyOverride(ref aoc);

            if (aocUpdatableItems.Count != 0)
            {
                foreach (Item item in aocUpdatableItems)
                {
                    ProcessAOC(item);
                }
                playerAnimator.runtimeAnimatorController = aoc;
            }
        }

        public void ProcessAOC(Item _item)
        {
            Debug.Log("Processing Item AOC: " + _item.itemName);

            List<KeyValuePair<AnimationClip, AnimationClip>> baseAOCOverrides = new List<KeyValuePair<AnimationClip, AnimationClip>>(aoc.overridesCount);
            aoc.GetOverrides(baseAOCOverrides);

            List<KeyValuePair<AnimationClip, AnimationClip>> aocOverridesToApply = new List<KeyValuePair<AnimationClip, AnimationClip>>(_item.animatorOverrideController.overridesCount);
            _item.animatorOverrideController.GetOverrides(aocOverridesToApply);

            Debug.Log(OverrideListToString(baseAOCOverrides));
            Debug.Log(OverrideListToString(aocOverridesToApply));
            switch (_item.equippableType)
            {
                case Item.EquippableType.Body:
                    aoc.ApplyOverrides(FindAndReplaceAnimations(ref baseAOCOverrides, ref aocOverridesToApply, "_Body"));
                    break;
                case Item.EquippableType.Hair:
                    aoc.ApplyOverrides(FindAndReplaceAnimations(ref baseAOCOverrides, ref aocOverridesToApply, "_Hair"));
                    break;
                case Item.EquippableType.Head:
                    aoc.ApplyOverrides(FindAndReplaceAnimations(ref baseAOCOverrides, ref aocOverridesToApply, "_Head"));
                    break;
            }
        }

        private void NullifyOverride(ref AnimatorOverrideController _input)
        {
            List<KeyValuePair<AnimationClip, AnimationClip>> buffer = new List<KeyValuePair<AnimationClip, AnimationClip>>(_input.overridesCount);
            _input.GetOverrides(buffer);

            for (int i=0; i< _input.overridesCount; i++)
            {
                buffer[i] = new KeyValuePair<AnimationClip, AnimationClip>(buffer[i].Key, null);
            }

            _input.ApplyOverrides(buffer);
        }

        public string OverrideListToString(List<KeyValuePair<AnimationClip, AnimationClip>> _input)
        {
            string returnString = "";
            foreach (KeyValuePair<AnimationClip, AnimationClip> keyValuePair in _input)
            {
                if (keyValuePair.Key != null && keyValuePair.Value != null)
                {
                    returnString += string.Format("<color=black>Key:</color>\t<color=blue>{0}</color>\t<color=red>\t| |\t</color>\t<color=black>Value:\t</color><color=blue>{1}</color>\n", keyValuePair.Key.name, keyValuePair.Value.name);
                }
                else
                {
                    if (keyValuePair.Key == null)
                    {
                        returnString += string.Format("<color=black>Key:</color>\t<color=blue>null</color>\t<color=red>\t| |\t</color>\t<color=black>Value:\t</color><color=blue>{0}</color>\n", keyValuePair.Value.name);
                    }
                    else
                    {
                        returnString += string.Format("<color=black>Key:</color>\t<color=blue>{0}</color>\t<color=red>\t| |\t</color>\t<color=black>Value:\t</color><color=blue>null</color>\n", keyValuePair.Key.name);
                    }
                }
            }
            return returnString;
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
