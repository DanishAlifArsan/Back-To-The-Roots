using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_alt : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector2 moveDelta;
    float movementX = 0.0f;
    float movementY = 0.0f;

    public float stepFactor = 0.05f;
    public float stopFactor = 0.1f;
    [SerializeField] private float walkingSpeed;
    [SerializeField] private float crouchingspeed;
    [SerializeField] private float sprintspeed;
    [SerializeField] private float maxStamina;
    private float maxSpeed;
    private float currentStamina;

    private bool isCrouching = false;
    private bool isSprinting = false;
    private bool isTired = false;
    

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        maxSpeed = walkingSpeed;
        currentStamina = maxStamina;
    }

    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (!isTired)
        {
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

            moveDelta = new Vector2(movementX, movementY);

            if (x > 0)
            {
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }
            else if (x < 0)
            {
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y);
            }

            transform.Translate(moveDelta * Time.deltaTime);
        }

        //kalau stamina nol, player gak bisa gerak untuk sementara
         if (currentStamina < 0)
        {
            isTired = true;
        } else if (currentStamina >= maxStamina) {
            isTired = false;
        }

        //stamina recover ketika player tidak crouch atau sprint
        if (!isCrouching && currentStamina < maxStamina && !isSprinting)
        {
            currentStamina += Time.deltaTime * 2;
        }
    }

    private void Update() {
        //membuat player crouch
        if (!isSprinting && !isTired)
        {
            if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0)
            {
                isCrouching = true;
                transform.gameObject.tag = "Hiding";
                maxSpeed = crouchingspeed;     
                currentStamina -= Time.deltaTime;
            }
            //cancel crouch
            if (Input.GetKeyUp(KeyCode.LeftShift) || currentStamina < 0)
            {
                isCrouching = false;
                transform.gameObject.tag = "Player";
                maxSpeed = walkingSpeed;
            }
        }
        
         //membuat player sprint
        if (!isCrouching && !isTired)
        {
            if (Input.GetKey(KeyCode.Space) && currentStamina > 0)
            {
                isSprinting = true;
                maxSpeed = sprintspeed;     
                currentStamina -= Time.deltaTime * 2;
            }
            //cancel sprint
            if (Input.GetKeyUp(KeyCode.Space) || currentStamina < 0)
            {
                isSprinting = false;
                maxSpeed = walkingSpeed;
            }
        }
    }

    private void LateUpdate() {
        Debug.Log(currentStamina);
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
