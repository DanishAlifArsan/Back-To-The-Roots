using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkingSpeed;
    [SerializeField] private float crouchingspeed;
    [SerializeField] private float sprintspeed;
    [SerializeField] private float maxStamina;
    private float speed;
    private float currentStamina;

    private float horizontalInput;
    private float verticalInput;

    private bool isMoving;
    private bool isCrouching = false;
    private bool isSprinting = false;
    
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    private Animator anim;
    
    // Start is called before the first frame update
    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        speed = walkingSpeed;
        currentStamina = maxStamina;
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log(currentStamina);

        if (currentStamina < 0)
        {
            body.bodyType = RigidbodyType2D.Static;
            anim.enabled = false;
        } else if (currentStamina >= maxStamina) {
            body.bodyType = RigidbodyType2D.Dynamic;
            anim.enabled = true;
        }

        //stamina recover ketika player tidak crouch atau sprint
        if (!isCrouching && currentStamina < maxStamina && !isSprinting)
        {
            currentStamina += Time.deltaTime * 2;
        }

         if (!isMoving)
         {
            //mendapatkan input yang diberikan. apakah horizontal atau vertikal
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            // //mengubah arah menghadap
            anim.SetFloat("MoveX", horizontalInput);
            anim.SetFloat("MoveY", verticalInput);

            //jika arah panah ditekan, karakter berjalan
            if (horizontalInput != 0 || verticalInput != 0)
            {
                StartCoroutine(Walk());
            }
         }

        //animasi berjalan
        anim.SetBool("isMoving", isMoving); 

        //membuat player crouch
        if (!isSprinting)
        {
            if (Input.GetKey("space") && currentStamina > 0)
            {
                isCrouching = true;
                transform.gameObject.tag = "Hiding";
                speed = crouchingspeed;     
                currentStamina -= Time.deltaTime;
            }
            //cancel crouch
            if (Input.GetKeyUp("space") || currentStamina < 0)
            {
                isCrouching = false;
                transform.gameObject.tag = "Player";
                speed = walkingSpeed;
            }
        }
        
         //membuat player sprint
        if (!isCrouching)
        {
            if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0)
            {
                isSprinting = true;
                speed = sprintspeed;     
                currentStamina -= Time.deltaTime * 2;
            }
            //cancel sprint
            if (Input.GetKeyUp(KeyCode.LeftShift) || currentStamina < 0)
            {
                isSprinting = false;
                speed = walkingSpeed;
            }
        }
    }

    //proses berjalan
    private IEnumerator Walk()
    {  
        isMoving = true;
        body.velocity = new Vector2(horizontalInput * speed, verticalInput * speed);
        yield return null;
        isMoving = false;
    }
}
