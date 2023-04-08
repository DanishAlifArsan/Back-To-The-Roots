using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{

    public UnityEvent interactAction;
    private int leverNumber;
    public GameObject leverManage;
    public GameObject Player;
    private bool inRange = false;
    private bool isActive = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (inRange && Input.GetKeyDown(KeyCode.Space))
            {
                interactAction.Invoke();
                Debug.Log("Lever Triggered");
            }

            if (leverManage.GetComponent<LeverManager>().isTriggered == true) { isActive = false; }
        }
    }

    private void FixedUpdate()
    {
        inRange = getDistance() < 2;
    }

    float getDistance()
    {
        return Mathf.Sqrt(Mathf.Pow((transform.position.x - Player.transform.position.x), 2) + Mathf.Pow((transform.position.y - Player.transform.position.y), 2));
    }
}
