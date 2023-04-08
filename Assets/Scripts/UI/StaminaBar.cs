using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Image filling;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        filling.fillAmount = player.GetComponent<PlayerMovement>().stamina / player.GetComponent<PlayerMovement>().maxStamina;
    }
}
