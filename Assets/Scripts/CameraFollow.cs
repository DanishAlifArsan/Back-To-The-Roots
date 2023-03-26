using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public float boundary;
    float targetX;
    float targetY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float deltaX = target.position.x - transform.position.x;
        float deltaY = target.position.y - transform.position.y;

        if(Mathf.Abs(deltaX) > boundary)
        {
            targetX = target.position.x - boundary * returnSign(deltaX);
        }

        if (Mathf.Abs(deltaY) > boundary)
        {
            targetY = target.position.y - boundary * returnSign(deltaY);
        }

        transform.position = new Vector3(targetX, targetY, -10f);
    }

    int returnSign(float num)
    {
        if (num >= 0)
            return 1;
        else
            return -1;
    }
}
