using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private float horizontalInput;
    private float verticalInput;
    private bool isMoving;
    
    private Rigidbody2D body;
    private Animator anim;

    // Start is called before the first frame update
    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
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

