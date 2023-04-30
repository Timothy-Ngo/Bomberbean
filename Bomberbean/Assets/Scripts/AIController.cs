using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public GameObject player;
    public Transform enemy;
    public float movementSpeed = 1f;

    private int rangeMin = 0;
    private int rangeMax = 3;
    private int actionRange = 4;
    private int movementRange = 4;
    private int movementNum = 0;
    private int actionNum = 0;
    private float spacesX = 0f;
    private float spacesZ = 0f;

    Vector3 targetPosition;


    // Start is called before the first frame update
    void Start()
    {
        spacesX = transform.position.x;
        spacesZ = transform.position.z;
        StartCoroutine(AIMovement());
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed*Time.deltaTime);
        //Vector3 newDirection = Vector3.RotateTowards(transform.forward, new Vector3(0, targetPosition.y, 0), movementSpeed*Time.deltaTime, 0.0f);
        //newDirection = new Vector3(0, newDirection.y, 0);
        //transform.rotation = Quaternion.LookRotation(newDirection);
    }

    IEnumerator AIMovement()
    {
        while(true)
        {
            yield return new WaitForSeconds(2);

            

            actionNum = Random.Range(rangeMin, actionRange);

            if (actionNum == 0) // move towards player
            {
                targetPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            }
            else // random movement 
            {
                movementNum = Random.Range(rangeMin, movementRange);

                if (movementNum == 0) // left
                {
                    //transform.Rotate(new Vector3(0, 90, 0));
                    spacesX += -(Random.Range(rangeMin, rangeMax)); // gets random # of spaces to move
                }
                else if (movementNum == 1) // right
                {
                    //transform.Rotate(new Vector3(0, -90, 0));
                    spacesX += Random.Range(rangeMin, rangeMax); // gets random # of spaces to move
                }
                else if (movementNum == 2) // up
                {
                    //transform.Rotate(new Vector3(0, 180, 0));
                    spacesZ += -(Random.Range(rangeMin, rangeMax)); // gets random # of spaces to move
                }
                else // down
                {
                    //transform.Rotate(new Vector3(0, 0, 0));
                    spacesZ += Random.Range(rangeMin, rangeMax); // gets random # of spaces to move
                }

                targetPosition = new Vector3(Mathf.Round(spacesX), transform.position.y, Mathf.Round(spacesZ));
            }
    
            if (targetPosition.x < transform.position.x)
            {
                transform.Rotate(new Vector3(0, 90, 0));
            }
            else if (targetPosition.x < transform.position.x)
            {
                transform.Rotate(new Vector3(0, -90, 0));
            }
            else if (targetPosition.z < transform.position.z)
            {
                transform.Rotate(new Vector3(0, 180, 0));
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, 0));
            }
            
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        targetPosition = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
        
        /*if (enemy.eulerAngles.y < 0) {
            //enemy.eulerAngles.y += 360f
            transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
        }
        else
        {
            //enemy.eulerAngles.y -= 180f;
            transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        } 
        */
        spacesX = Mathf.Round(transform.position.x);
        spacesZ = Mathf.Round(transform.position.z);
    }
}
