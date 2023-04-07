using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        //player memeriksa item di sekitarnya
        Collider2D[] collectibles = Physics2D.OverlapCircleAll(transform.position, 1f, LayerMask.GetMask("Default"));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var col in collectibles)
            {
                //mengecek apakah item di sekitar termasuk collectible
                if (col.GetComponent<Collider2D>() != null && col.GetComponent<Collider2D>().gameObject.CompareTag("Collectible"))
                {
                    //mengecek apakah inventory full
                    if (!FindObjectOfType<Inventory>().isFull())
                    {
                        //secara normal menambahkan item ke inventory
                        FindObjectOfType<Inventory>().PickUp(col.GetComponent<Item>());
                        Destroy(col.gameObject); 
                    } else
                    {
                        Debug.Log("Inventory full");
                    }
                     
                }
            }                      
        }

        //player melempar batu
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //fungsi lempar batu di sini
            FindObjectOfType<Inventory>().Consume("Stone");
             
        }
    }
}
