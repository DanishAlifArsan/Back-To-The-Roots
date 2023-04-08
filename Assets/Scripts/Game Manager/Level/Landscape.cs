using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Landscape : MonoBehaviour
{
    [SerializeField] Transform teleporter;
    private DialogueTrigger fairyDialogue;
    private bool dialogueStarted = false;
    // Start is called before the first frame update
    private void Start()
    {
        fairyDialogue = GetComponent<DialogueTrigger>();
    }

    // Update is called once per frame
    private void Update()
    {
         if (!dialogueStarted)
         {
            fairyDialogue.dialBox.GetComponent<Dialogue>().lines = fairyDialogue.lines;
            fairyDialogue.interactAction.Invoke();
            fairyDialogue.gameObject.SetActive(false);
         }
    }
}
