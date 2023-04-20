using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Transform player;
    public float movementSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("d")) 
        {
            transform.position = player.position + new Vector3(movementSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey("a")) 
        {
            transform.position = player.position - new Vector3(movementSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey("w")) 
        {
            transform.position = player.position + new Vector3(0, 0, movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey("s")) 
        {
            transform.position = player.position - new Vector3(0, 0, movementSpeed * Time.deltaTime);
        }
    }
}
