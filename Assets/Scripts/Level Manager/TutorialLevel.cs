using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevel : MonoBehaviour
{
    [SerializeField] private DialogueTrigger[] tutorialDialogue;
    private bool[] isActive = {false,false,false};
    // Start is called before the first frame update
    void Start()
    {
        tutorialDialogue[0].dialBox.GetComponent<Dialogue>().lines = tutorialDialogue[0].lines;
        tutorialDialogue[0].interactAction.Invoke();
        tutorialDialogue[0].enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        for (int i = 1; i < tutorialDialogue.Length; i++)
        {
            tutorial(i);
        }
        
    }

    private void tutorial(int index) {
        if (!isActive[index] && Vector2.Distance(tutorialDialogue[index].Player.transform.position, tutorialDialogue[index].transform.position)  < 2f)
        {
            tutorialDialogue[index].dialBox.GetComponent<Dialogue>().lines = tutorialDialogue[index].lines;
            tutorialDialogue[index].interactAction.Invoke();
            tutorialDialogue[index].enabled = false;
            isActive[index] = true;
        }
    }
}
