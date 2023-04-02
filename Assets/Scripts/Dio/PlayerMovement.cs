using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour

    
{
    private BoxCollider2D boxCollider;
    private Vector2 moveDelta;
    private bool staminaCooldown = false;
    private float timeCounter = 0.0f;
    public bool canMove = true;
    bool isSprinting = false;
    bool isCrouching = false;
    private bool isTired;
    float movementX = 0.0f;
    float movementY = 0.0f;
    public float stamina;
    float stepFactor;
    float maxSpeed;

    public float walkAccel = 0.05f;
    public float crouchAccel = 0.05f;
    public float sprintAccel = 0.2f;
    public float walkSpeed = 1;
    public float crouchSpeed = 0.5f;
    public float sprintSpeed = 2;
    public float maxStamina = 5;
    public float staminaRegen = 1;
    public float staminaDrain = 1;
    public float staminaCooldTime = 2;
    public float stopFactor = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        stamina = maxStamina;
    }

    private void Update()
    {
         //kalau stamina nol, player gak bisa gerak untuk sementara
        if (stamina < 0)
        {
            isSprinting = false;
            isCrouching = false;
            isTired = true;
            staminaCooldown = false;
        } else if (stamina >= maxStamina) {
            isTired = false;
        }

        // Sprint handler
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0) 
        {
            // Change speed, drain stamina, and trigger staminaRegen cooldown when sprinting
            stepFactor = sprintAccel;
            maxSpeed = sprintSpeed;
            stamina -= staminaDrain * Time.deltaTime * 2;
            staminaCooldown = true;
            timeCounter = 0;
            isSprinting = true;
            isCrouching = false;
        }
        else if (Input.GetKey(KeyCode.LeftControl) && stamina > 0)
        {
            // Change speed, drain stamina, and change state when crouching
            stepFactor = crouchAccel;
            maxSpeed = crouchSpeed;
            stamina -= staminaDrain * Time.deltaTime;
            staminaCooldown = true;
            timeCounter = 0;
            isSprinting = false;
            isCrouching = true;
            transform.gameObject.tag = "Hiding";
        }
        else
        {
            // Change speed back to walk when not sprinting
            stepFactor = walkAccel;
            maxSpeed = walkSpeed;
            isSprinting = false;
            isCrouching = false;
            transform.gameObject.tag = "Player";
        }

        if (!isSprinting && !isCrouching)
        {
            // staminaRegen cooldown handler
            if (timeCounter < staminaCooldTime)
            {
                timeCounter += Time.deltaTime;
            }
            else
            {
                timeCounter = 0.0f;
                staminaCooldown = false;
            }

            // staminaRegen handler
            if (stamina < maxStamina && !staminaCooldown)
                stamina += staminaRegen * Time.deltaTime;
            else if (stamina > maxStamina)
                stamina = maxStamina;
        }
    }

    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // X axis acceleration handler 
        if (x != 0 && Mathf.Abs(movementX) <= maxSpeed)
        {
            movementX = movementX + (x * stepFactor);
        }
        else if (Mathf.Abs(movementX) > 0 && x == 0)
        {
            movementX = movementX - (stopFactor * returnSign(movementX));
        }

        // X axis movement cleanup
        if (Mathf.Abs(movementX) < stepFactor / 2)
            movementX = 0;
        if (Mathf.Abs(movementX) >= maxSpeed)
            movementX = returnSign(movementX) * maxSpeed;

        // Y axis acceleration handler
        if (y != 0 && Mathf.Abs(movementY) <= maxSpeed)
        {
            movementY = movementY + (y * stepFactor);
        }
        else if (Mathf.Abs(movementY) > 0 && y == 0)
        {
            movementY = movementY - (stopFactor * returnSign(movementY));
        }

        //Y axis movement cleanup
        if (Mathf.Abs(movementY) < stepFactor / 2)
            movementY = 0;
        if (Mathf.Abs(movementY) >= maxSpeed)
            movementY = returnSign(movementY) * maxSpeed;

        // Change movement vector, Zero movement when in an event (via canMove)
        if (canMove && !isTired) { moveDelta = new Vector2(movementX, movementY); }
        else { moveDelta = new Vector2(0, 0);  }

        // Rotation handler
        if (x > 0 && canMove)
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else if (x < 0 && canMove)
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y);
        }


        // Move the character via Translate
        transform.Translate(moveDelta * Time.deltaTime);
    }

    int returnSign(float num)
    {
        if(num >= 0)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

}
