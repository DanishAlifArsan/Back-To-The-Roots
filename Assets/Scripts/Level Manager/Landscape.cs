using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Landscape : MonoBehaviour
{
    private DialogueTrigger fairyDialogue;
    private bool isTriggered = false;
    private bool dialogueStarted = false;
    // Start is called before the first frame update
    private void Start()
    {
        fairyDialogue = GetComponent<DialogueTrigger>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (fairyDialogue.dialBox.GetComponent<Dialogue>().isStarted)
        {
            dialogueStarted = true;
        }

        if (dialogueStarted)
        {
            if (!fairyDialogue.dialBox.GetComponent<Dialogue>().gameObject.activeSelf)
            {
                SceneManager.LoadScene(3);
            }
        }   
    }
}
