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

    // Update is called once per frame
    private void Update()
    {
        if (!DetectPlayer())
        {    
            //hal yg dilakukan ketika sampai ke waypoints saat ini
            if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
            {
                // 1. mendapatkan waypoints selanjutnya dan berhenti
                currentWaypointIndex++;
                      
                // 2. jika sampai ke waypoints terakhir, waypoints selanjutnya adalah waypoints pertama
                if (currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex = 0;
                }
            }
                
            //gerak ke arah waypoints saat ini
            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
        }
    }

    private bool DetectPlayer()
    {
        // Mendapatkan semua objek yang masuk ke vision
        Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, vision, playerLayer);

        foreach (Collider2D player in players)
        {
            // Cek apakah ada player di vision
            RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position, vision, playerLayer);
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
            {
                // mengejar ke arah player
                StartCoroutine(Chase(player, hit));
            }
        }

        return isChasing;
    }

    void OnDrawGizmosSelected()
    {
        // menggambar vision di editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, vision);
    }

    //membuat penjaga mengejar player selama 1 detik ketika player ada di range penjaga
    private IEnumerator Chase(Collider2D player, RaycastHit2D hit)
    {  
        isChasing = true;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        yield return new WaitForSeconds(2);
        isChasing = false;
    }
}
