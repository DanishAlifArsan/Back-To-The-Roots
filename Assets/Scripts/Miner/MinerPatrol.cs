using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerPatrol : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float speed;
    [SerializeField] private float vision;
    [SerializeField] private LayerMask playerLayer;

    private int currentWaypointIndex = 0;
    private bool isChasing = false;

    Vector3 prevLocation = Vector3.zero;

    // Update is called once per frame
    private void Update()
    {
        //mendapatkan arah gerak enemy saat ini
        Vector3 curVelocity  = (transform.position - prevLocation);

        prevLocation = transform.position;

        if (!DetectPlayer())
        {
            //gerak ke arah waypoints saat ini
            if (transform.position != waypoints[currentWaypointIndex].transform.position){
                transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
            } else {
                //hal yg dilakukan ketika sampai ke waypoints saat ini
                if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
                {
                    // 1. mendapatkan waypoints selanjutnya
                    currentWaypointIndex++;
                                
                    // 2. jika sampai ke waypoints terakhir, waypoints selanjutnya adalah waypoints pertama
                    if (currentWaypointIndex >= waypoints.Length)
                    {
                        currentWaypointIndex = 0;
                    }
                }
            }    
        }     
    }

    private bool DetectPlayer()
    {
        // Membuat vision penjaga
        Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, vision, playerLayer);

        foreach (Collider2D player in players)
        {
            // Mendeteksi player di area vision
            RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position, vision, playerLayer);
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
            {
                StartCoroutine(Chase(player));
            }
        }

        return isChasing;
    }
    
    //menggambar vision penjaga di editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, vision);
    }

    //membuat penjaga mengejar player selama 1 detik ketika player ada di range penjaga
    private IEnumerator Chase(Collider2D player)
    {  
        isChasing = true;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        yield return new WaitForSeconds(1);
        isChasing = false;
    }
}
