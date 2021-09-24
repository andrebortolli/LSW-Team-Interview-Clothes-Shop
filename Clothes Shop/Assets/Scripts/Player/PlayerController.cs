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
        public class OnInteractableWithinReach : UnityEvent<Interactable> { }
        [Serializable]
        public class OnInteractableOutOfReach : UnityEvent<Interactable> { }

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
            foreach(Item item in playerData.data.inventory.EquippedItems)
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
