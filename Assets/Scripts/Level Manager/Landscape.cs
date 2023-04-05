using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Landscape : MonoBehaviour
{
    private DialogueTrigger fairyDialogue;
    private bool isTriggered = false;
    // Start is called before the first frame update
    private void Start()
    {
        fairyDialogue = GetComponent<DialogueTrigger>();
    }

    // Update is called once per frame
    private void Update()
    {
        // if (fairyDialogue.Player.GetComponent<PlayerMovement>().canMove)
        // {
        //     SceneManager.LoadScene(2);
        // }
        // if(Input.GetKeyDown(KeyCode.Space) && !isTriggered)
        // {
        //     if (Vector2.Distance(transform.position, fairyDialogue.Player.transform.position) < 1f)
        //     {
        //         fairyDialogue.dialBox.GetComponent<Dialogue>().lines = fairyDialogue.lines;
        //         fairyDialogue.interactAction.Invoke();
        //         fairyDialogue.enabled = false;
                
        //     }
        // }
        
    }

    // private void LateUpdate() {
    //     if (isTriggered)
    //     {
    //         SceneManager.LoadScene(2);
    //     }
    // }
}
