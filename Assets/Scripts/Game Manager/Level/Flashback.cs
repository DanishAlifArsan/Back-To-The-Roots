using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Flashback : MonoBehaviour
{
    [SerializeField] private RectTransform background; 
    [SerializeField] private DialogueTrigger dialogue;
    private bool dialogActive = false;
    private bool dialogEnds = false;
    float curZoom = 2;
    float zoomRate = .2f;
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!dialogActive)
        {
            dialogue.dialBox.GetComponent<Dialogue>().lines = dialogue.lines;
            dialogue.interactAction.Invoke();
            dialogue.gameObject.SetActive(false);
            dialogActive = true;
            dialogEnds = true;
        }  

        if(dialogEnds && !dialogue.dialBox.GetComponent<Dialogue>().gameObject.activeSelf) {
            SceneManager.LoadScene(3);
        }
    }

    void FixedUpdate() {
        background.localScale = new Vector2(curZoom,curZoom);
        if (curZoom > 1)
        {
            curZoom -= Time.deltaTime * zoomRate;
        }
    }
}
