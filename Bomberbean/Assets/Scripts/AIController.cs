using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public GameObject player;
    public float movementSpeed = 1f;
    public float movementFrequency = 2f;

    private int rangeMin = 0;
    private int rangeMax = 3;
    private int actionRange = 4;
    private int movementRange = 4;
    private int movementNum = 0;
    private int actionNum = 0;
    private float spacesX = 0f;
    private float spacesZ = 0f;
    private bool isMoving = false;
    private float step;
    private int prevAction = 0;
    private Vector3[] directions = {Vector3.forward, Vector3.left, Vector3.back, Vector3.right};
    private float maxDistanceCheck = 1f;
    private bool beingHit;

    public Vector3 targetPosition, movement, prevPosition;

    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;
        spacesX = transform.position.x;
        spacesZ = transform.position.z;
        prevPosition = transform.position;
        beingHit = false;
        //StartCoroutine(AIMovement());
        StartCoroutine(ChoosePath());
    }

    /*
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed*Time.deltaTime);
    }
    */
    void Update() 
    {
        if (isMoving)
        {
            step = movementSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            if (ApproximatelyNear(transform.position, targetPosition, 0.0001f))
            {
                transform.position = targetPosition;
                isMoving = false;
                prevPosition = targetPosition;
                StartCoroutine(ChoosePath());
            }
        }
    }
    
    IEnumerator ChoosePath()
    {
        yield return new WaitForSeconds(Random.Range(0,3));
        beingHit = false;
        List<Vector3> openPaths = FindOpenPaths();
        if (openPaths.Count > 0)
        {
            actionNum = Random.Range(rangeMin, openPaths.Count);
            Debug.Log("Action num: " + actionNum);
            targetPosition = transform.position + openPaths[actionNum];

            isMoving = true;
        }
    }

    private List<Vector3> FindOpenPaths() // Return directions(Vector3) that AI can be moved to 
    {
        List<Vector3> openPaths = new List<Vector3>();
        foreach (Vector3 direction in directions)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, maxDistanceCheck))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    openPaths.Add(direction);
                }
                Debug.Log(hit.collider.name);
            }
            else if (Physics.Raycast(transform.position, direction, out hit, maxDistanceCheck*2))
            {
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    Debug.Log(hit.collider.name);
                }
                else 
                {
                    openPaths.Add(direction);
                }

            }
            else 
            {
                openPaths.Add(direction);
            }
        }
        return openPaths;

    }

    private bool ApproximatelyNear(Vector3 posA, Vector3 posB, float precision)
    {
        return (Vector3.Distance(posA, posB) < precision);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            targetPosition = prevPosition;
            beingHit = true;
        }
    }

    IEnumerator AIMovement()
    {
        int prevAction = 0;
        while(true)
        {
            yield return new WaitForSeconds(movementFrequency);

            

            actionNum = Random.Range(rangeMin, actionRange);
            if (actionNum == prevAction)
            {
                actionNum++;
                
            }


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

                targetPosition = new Vector3(Mathf.Round(spacesX), 1, Mathf.Round(spacesZ));
                
            } 
        }
    }
    /*
    void OnCollisionEnter(Collision collision)
    {
        targetPosition = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
        
        spacesX = Mathf.Round(transform.position.x);
        spacesZ = Mathf.Round(transform.position.z);
    }
    */
}