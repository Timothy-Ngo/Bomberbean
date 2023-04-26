using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public Transform enemy;
    public float movementSpeed = 1f;

    private int rangeMin = 0;
    private int rangeMax = 3;
    private int actionRange = 4;
    private int actionNum = 0;
    private float spacesX = 0f;
    private float spacesZ = 0f;

    Vector3 targetPosition;

    private Rigidbody body;


    // Start is called before the first frame update
    void Start()
    {
        spacesX = transform.position.x;
        spacesZ = transform.position.z;
        body = GetComponent<Rigidbody>();
        StartCoroutine(AIMovement());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed*Time.deltaTime);
        
    }

    IEnumerator AIMovement()
    {
        while(true)
        {
            yield return new WaitForSeconds(2);

            actionNum = Random.Range(rangeMin, actionRange);

            if (actionNum == 0) // left
            {
                spacesX += -(Random.Range(rangeMin, rangeMax)); // gets random # of spaces to move
            }
            else if (actionNum == 1) // right
            {
                spacesX += Random.Range(rangeMin, rangeMax); // gets random # of spaces to move
            }
            else if (actionNum == 2) // up
            {
                spacesZ += -(Random.Range(rangeMin, rangeMax)); // gets random # of spaces to move
            }
            else // down
            {
                spacesZ += Random.Range(rangeMin, rangeMax); // gets random # of spaces to move
            }

            targetPosition = new Vector3(Mathf.Round(spacesX), transform.position.y, Mathf.Round(spacesZ));
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        targetPosition = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
        
        spacesX = Mathf.Round(transform.position.x);
        spacesZ = Mathf.Round(transform.position.z);
    }
}
