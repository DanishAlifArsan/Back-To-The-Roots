using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevel : MonoBehaviour
{
    [SerializeField] private DialogueTrigger[] tutorialDialogue;
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private GameObject[] invisibleWall;
    [SerializeField] private GameObject[] interactObject;
    private bool[] isTriggered = {false,false,false,false,false,false};

    private bool dialogueStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var i in tutorialDialogue)
        {
            i.gameObject.SetActive(false);
        }
        foreach (var i in invisibleWall)
        {
            i.SetActive(false);
        }
        
        blackScreen.SetActive(false);
        interactObject[0].SetActive(false);
        tutorialDialogue[0].gameObject.SetActive(true);

        tutorialDialogue[0].dialBox.GetComponent<Dialogue>().lines = tutorialDialogue[0].lines;
        tutorialDialogue[0].interactAction.Invoke();
        tutorialDialogue[0].gameObject.SetActive(false);

        tutorialDialogue[1].gameObject.SetActive(true);

        invisibleWall[0].SetActive(true);
    }

    // Update is called once per frame
    private void Update()
    {
        for (int i = 1; i < tutorialDialogue.Length; i++)
        {
            tutorial(i);
        }

        // if (tutorialDialogue[3].gameObject.activeSelf)
        // {
        //     StartCoroutine(fadeScreen(1));
        // }
    }

    private void tutorial(int index) {
        if (!isTriggered[index] && Vector2.Distance(tutorialDialogue[index].Player.transform.position, tutorialDialogue[index].transform.position)  < 2f)
        {
            tutorialDialogue[index].dialBox.GetComponent<Dialogue>().lines = tutorialDialogue[index].lines;
            tutorialDialogue[index].interactAction.Invoke();
            tutorialDialogue[index].gameObject.SetActive(false);
            isTriggered[index] = true;
            if (index == 4)
            {
                interactObject[0].SetActive(true);
            }
            if (index == 5)
            {
                interactObject[1].SetActive(true);
            }
        }
        // if (!tutorialDialogue[index].enabled)
        // {
        //     if (index != tutorialDialogue.Length-1)
        //     {
        //         tutorialDialogue[index+1].gameObject.SetActive(true);
        //     }
        // }
    }

    public void startFadeScreen() {
        StartCoroutine(fadeScreen(1));
    }

    private IEnumerator fadeScreen(int duration) {
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(duration);
        blackScreen.SetActive(false);
    }
}
