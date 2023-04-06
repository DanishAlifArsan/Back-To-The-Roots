using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform camera;
    [SerializeField] private DialogueTrigger endDialogue;
    
    private bool isTriggered = false;
    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(player.position, endDialogue.transform.position) < 1f)
        {
            camera.GetComponent<CameraFollow>().enabled= false;
            camera.position = Vector3.MoveTowards(camera.position, new Vector3(0,0,-10), Time.deltaTime);
            if (!isTriggered)
            {
                endDialogue.dialBox.GetComponent<Dialogue>().lines = endDialogue.lines;
                endDialogue.interactAction.Invoke();
                endDialogue.enabled = false;
                isTriggered = true;
            }
        } else {
            player.Translate(Vector2.up * Time.deltaTime);
        }
    }
}
