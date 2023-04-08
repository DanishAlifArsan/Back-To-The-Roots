using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MinerPatrol : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float speed;
    [SerializeField] private float vision;
    [SerializeField] private float deadzone;
    //[SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform player;
    [SerializeField] private float bustedTime;

    [SerializeField] private GameObject gameoverScreen;

    private Vector3 prevLocation = Vector3.zero;

    private int currentWaypointIndex = 0;
    private float nextWaypointDist = 1f;
    private float timeCounter = 0;
    private bool isChasing = false;

    Path path;
    int currAstarWay = 0;
    bool reachedEndofPath = false;
    Seeker seeker;

    private void Start() {
        // prevLocation = transform.position;
        seeker = GetComponent<Seeker>();
    }

    void onPathComplete(Path p)
    {
        if (!p.error)
        {
            Debug.Log("Path is calculated");
            path = p;
            currAstarWay = 1;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //dijelasin di video laporan minggu 2
        Vector3 curVelocity  = (transform.position - prevLocation);

        if (curVelocity.x < 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
        } else if (curVelocity.x > 0) {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * 1, transform.localScale.y, transform.localScale.z);
        }

        prevLocation = transform.position;
        //sampai sini
        
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

            StartCoroutine(findPathtoWaypoint());
            Pathfinding();
        }
    }

    private void Update()
    {
        bool isBusted = false;

        if (isChasing &&!isBusted)
        {

            if(Vector2.Distance(transform.position, player.position) <= deadzone) { timeCounter += Time.deltaTime; }
            else { timeCounter = 0; }

            if(timeCounter > bustedTime)
            {
                Time.timeScale = 0;
                gameoverScreen.SetActive(true);
                isBusted = true;
            }
        }

        Debug.Log(waypoints[currentWaypointIndex].transform.position);
        Debug.Log(transform.position);
    }

    private void Pathfinding()
    {
        if(path == null) { return; }
        
        if(currAstarWay >= path.vectorPath.Count)
        {
            reachedEndofPath = true;
            return;
        }
        else { reachedEndofPath = false; }

        Vector2 direction = (Vector2)(path.vectorPath[currAstarWay] - transform.position).normalized;

        transform.Translate(direction * speed * Time.deltaTime);

        float distance = Vector2.Distance(transform.position, path.vectorPath[currAstarWay]);

        if(distance < nextWaypointDist)
        {
            currAstarWay++;
        }
    }

    private bool DetectPlayer()
    {
        /*
        // Mendapatkan semua objek yang masuk ke vision
        Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, vision, playerLayer);

        foreach (Collider2D player in players)
        {
            // Cek apakah ada player di vision
            RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position, vision, playerLayer);
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
            {
                // mengejar ke arah player
                StartCoroutine(Chase(player));
            }
        }

        return isChasing;
        */

        float distanceToTarget = Vector2.Distance(transform.position, player.position);
        if(distanceToTarget < vision)
        {
            StartCoroutine(Chase(player));
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
    private IEnumerator Chase(Transform player)
    {  
        isChasing = true;
        StartCoroutine(findPathtoPlayer());
        Pathfinding();
        yield return new WaitForSeconds(2);
        isChasing = false;
        /*
        //dijelasin di video laporan minggu 2
        if (!isChasing && player.GetComponent<Collider2D>().gameObject.CompareTag("Player"))
        {
            if (player.transform.position.x < transform.position.x + vision && player.transform.position.x > transform.position.x - vision)
            {
                if (player.transform.position.y < transform.position.y + vision && player.transform.position.y > transform.position.y - vision)
                {
                    Time.timeScale = 0;
                    gameoverScreen.SetActive(true);
                }
            }   
        } 
        //sampai sini
        */
    }

    private IEnumerator findPathtoPlayer()
    {
        seeker.StartPath(transform.position, player.position, onPathComplete);
        yield return new WaitForSeconds(1);
    }

    private IEnumerator findPathtoWaypoint()
    {
        seeker.StartPath(transform.position, waypoints[currentWaypointIndex].transform.position, onPathComplete);
        yield return new WaitForSeconds(1);
    }
}
