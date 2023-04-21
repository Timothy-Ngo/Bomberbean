using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMechanics : MonoBehaviour
{
    [Tooltip("Prefab for bomb")]
    public GameObject bombPrefab;
    [Tooltip("Number of layer that bomb explosion destroys")]
    public int layerNum = 7;
    [Tooltip("How long it takes for the bomb to explode")]
    public float fuseTime = 2.5f;
    [Tooltip("Explosion range")]
    public float rayLength = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        layerMask = 1 << layerNum;   
    }

    private GameObject playerBomb;
    private Vector3 bombSpawn;
    private RaycastHit explosion;
    private int layerMask;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Bomb Placed");
            Debug.Log(transform.position);
            Debug.Log(new Vector3(Mathf.Round(transform.position.x), 1, Mathf.Round(transform.position.z)));
            playerBomb = Instantiate(bombPrefab, new Vector3(Mathf.Round(transform.position.x), 1, Mathf.Round(transform.position.z)), Quaternion.identity);
            //StartCoroutine(Explosion());
            Explosion(playerBomb, fuseTime, layerMask);
        }   
       
        
        
    }
    


    private List<Vector3> directions = new List<Vector3>(){Vector3.forward, Vector3.back, Vector3.left, Vector3.right};
    void Explosion(GameObject obj, float destroyTime, int collisionLayer)
    {
        Destroy(obj, destroyTime);
        foreach(Vector3 direction in directions)
        {
            if (Physics.Raycast(obj.transform.position, direction, out explosion, rayLength, collisionLayer))
            {
                Debug.Log("Collided with " + explosion.collider.gameObject.name);
                Destroy(explosion.collider.gameObject, destroyTime);
            }
        }
        
        
    }
}
