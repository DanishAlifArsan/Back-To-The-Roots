using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{

    private float stoneHold;

    // Start is called before the first frame update
    private void Start()
    {
        stoneHold = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        //player mengambil stone yang tersebar di map
        Collider2D stone = Physics2D.OverlapCircle(transform.position, 0.2f, LayerMask.GetMask("Default"));

        if (Input.GetKeyDown(KeyCode.Space))
        {
           if (stone.GetComponent<Collider2D>() != null && stone.GetComponent<Collider2D>().gameObject.CompareTag("Collectible"))
            {
                if (stoneHold < 2)
                {
                    stoneHold += 1; 
                    Destroy(stone.gameObject);
                } else {
                    Debug.Log("Maximal number of stone");
                }
            } 
        }

        Debug.Log(stoneHold);

        //player melempar batu
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //fungsi lempar batu di sini
            if (stoneHold > 0)
            {
                stoneHold -= 1;
            }
        }
    }
}
