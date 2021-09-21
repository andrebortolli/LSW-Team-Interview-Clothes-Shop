using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClothesShop.SO.Player;
using ScriptableObjectExtensions.Variables;

public class PlayerController : MonoBehaviour
{
    public Player playerData;
    public FloatVariable walkingSpeed;

    private Animator playerAnimator;
    private Rigidbody2D playerRigidbody;

    #region Animator Parameters

    [Header("Animator Parameters")]
    public string horizontalMovementParameterName;
    public string verticalMovementParameterName;

    #endregion

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(playerData.id + playerData.playerName.Value);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        PlayerMovement();
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
        playerRigidbody.velocity = new Vector2(movementAxesValues.x * walkingSpeed.Value, movementAxesValues.y * walkingSpeed.Value) * Time.fixedDeltaTime;

        //Update animator parameters
        playerAnimator.SetFloat(horizontalMovementParameterName, movementAxesValues.x);
        playerAnimator.SetFloat(verticalMovementParameterName, movementAxesValues.y);
    }
}
