using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject dialBox;
    public GameObject Player;
    public string[] lines;
    private bool inRange = false;
    private bool isTriggered = false;
    public UnityEvent interactAction;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Check if player is in range
        if(inRange)
        {
            // Trigger dialogue script on button press if its not triggered
            if(Input.GetKeyDown(KeyCode.Space) && !isTriggered)
            {
                dialBox.GetComponent<Dialogue>().lines = lines;
                interactAction.Invoke();
                Debug.Log("Invoked");
            }
        }

        // Mark as triggered and lock the player movement (Return it when dialogue is done)
        if (dialBox.GetComponent<Dialogue>().isStarted) { isTriggered = true; Player.GetComponent<PlayerMovement>().canMove = false;  }
        else { isTriggered = false; Player.GetComponent<PlayerMovement>().canMove = true;  }
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
