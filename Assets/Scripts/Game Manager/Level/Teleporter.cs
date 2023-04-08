using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private string location;
    private void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player"))
        {
            switch (location)
            {
                case "Landscape":
                    SceneManager.LoadScene(2);
                    break;
                case "Mine":
                    SceneManager.LoadScene(4);
                    break;
            }
            
        }
    }
}
