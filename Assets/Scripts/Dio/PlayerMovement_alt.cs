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
    public float maxSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
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
