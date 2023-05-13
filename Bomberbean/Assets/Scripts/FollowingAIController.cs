using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingAIController : MonoBehaviour
{
    [SerializeField] private bool isMoving;
    [SerializeField] private float approximationTolerance = 0.0001f;
    private float maxDistanceCheck = 1f;
    private Vector3[] directions = {Vector3.forward, Vector3.left, Vector3.back, Vector3.right};

    private Vector3 bestPath;
    private Vector3 prevPath;
    private float step;

    public GameObject player;
    public float movementSpeed = 1f;
    public Vector3 targetPosition;

    void Start()
    {
        isMoving = false;
        bestPath = Vector3.zero;
        prevPath = Vector3.zero;
       
    }

    void Update()
    {
        if (isMoving)
        {
            step = movementSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            if (ApproximatelyNear(transform.position, targetPosition, approximationTolerance))
            {
                transform.position = targetPosition;
                isMoving = false;
            }
            
        }
        else
        {
            List<Vector3> openPaths = FindOpenPaths();
            int i = 1;
            foreach (Vector3 openPath in openPaths)
            {
                Debug.Log("" + (i++) + " = " +  openPath);
            }
            if (openPaths.Count == 0) 
            {
                ;
            }
            else if (openPaths.Count == 1) // If only one path is open then default to move there
            {
                Debug.Log("Only one Path: " + openPaths[0]);
                targetPosition = transform.position + openPaths[0];
                isMoving = true;
                prevPath = openPaths[0];
            }
            else
            {
                // Find vector in the direction of the player 
                Vector3 playerDirVec = new Vector3(player.transform.position.x - transform.position.x, transform.position.y, player.transform.position.z - transform.position.z);
                
                // Find angle from pos-x-axis in direction of the player
                float angleToPlayer = FindClampedAngle(playerDirVec);   
                Debug.Log("angleToPlayer: " + angleToPlayer);

                // Debug direction vector and angle
                Debug.DrawLine(transform.position , new Vector3(Mathf.RoundToInt(transform.position.x + playerDirVec.x), transform.position.y, Mathf.RoundToInt(transform.position.z + playerDirVec.z)) , Color.magenta, 4f);
                Debug.Log(angleToPlayer);

                // Check which of the available directions to move in is the closest angle to the player
                float angleDiff = Mathf.Infinity;
                
                foreach (Vector3 path in openPaths)
                {
                    // Determine how close the angleToPlayer is to a cardinal direction
                    float pathDiff;
                    if (path == Vector3.right)
                    {
                        if (angleToPlayer >= 180)
                            pathDiff = 360 - angleToPlayer;
                        else
                            pathDiff = angleToPlayer;

                    }
                    else
                    {
                        pathDiff = Mathf.Abs(FindClampedAngle(path) - angleToPlayer);
                    }

                    Debug.Log("Currently Checking Path: " + path);
                    if (path == -(prevPath))
                    {
                        ;
                    }
                    else
                    {
                        if (pathDiff <= angleDiff )
                        {
                            Debug.Log("Current best path: " + path);
                            angleDiff = pathDiff;
                            bestPath = path;
                        }
                    }
                }
                targetPosition = transform.position + bestPath;
                isMoving = true;
                prevPath = bestPath;

            }

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

    private float FindClampedAngle(Vector3 dir) // Finds an angle from the x-axis and clamps it between 0-359 (deg)
    {
        return (((Mathf.Atan2(Mathf.RoundToInt(dir.z), Mathf.RoundToInt(dir.x)) * Mathf.Rad2Deg) + 360) % 360);
    }

}
