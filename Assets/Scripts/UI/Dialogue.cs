using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float swipeSpeed;

    private int index;
    public bool isStarted = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Text scrolling
        if (Input.GetKeyDown(KeyCode.Space) && isStarted)
        {
            if (textComponent.text == lines[index])
            {
                nextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    // Trigger method
    public void startDialogue()
    {
        isStarted = true;
        gameObject.SetActive(true);
        textComponent.text = string.Empty;
        index = 0;
        StartCoroutine(typeLines());
    }

    // Text swiping
    IEnumerator typeLines()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(swipeSpeed);
        }
    }

    public void nextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(typeLines());
        }
        else
        {
            gameObject.SetActive(false);
            isStarted = false;
        }
    }
}
