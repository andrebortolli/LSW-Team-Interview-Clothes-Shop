using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClothesShop.SO.Player;
using ScriptableObjectExtensions.Variables;

public class PlayerController : MonoBehaviour
{
    public Player playerData;
    public FloatVariable walkingSpeed;

    private Rigidbody2D playerRigidbody;

    private void Awake()
    {
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

    private void PlayerMovement()
    {
        Vector2 movementAxesValues = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        playerRigidbody.velocity = new Vector2(movementAxesValues.x * walkingSpeed.Value, movementAxesValues.y * walkingSpeed.Value) * Time.fixedDeltaTime;
    }
}
