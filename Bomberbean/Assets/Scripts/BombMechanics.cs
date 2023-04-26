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
    [Tooltip("Max amount of bombs the player can regenerate")]
    public int maxBombs = 2;
    [Tooltip("Cooldown until another bomb is regenerated for player ")]
    public float maxCooldown = 3.0f;
    public GameObject uiObject;
    private BombUI bombUI;
    private int currentBombs;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = 1 << layerNum;
        currentBombs = maxBombs;
        currentCooldown = maxCooldown;
        bombUI = uiObject.GetComponentInChildren<BombUI>();
    }

    private GameObject playerBomb;
    private Vector3 bombSpawn;
    private RaycastHit explosion;
    private int layerMask;
    private float currentCooldown;
    private float cooldownPercent;

    public UIController uiController;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentBombs > 0)
            {
                //Debug.Log(transform.position);
                //Debug.Log(new Vector3(Mathf.Round(transform.position.x), 1, Mathf.Round(transform.position.z)));
                playerBomb = Instantiate(bombPrefab, new Vector3(Mathf.Round(transform.position.x), 1, Mathf.Round(transform.position.z)), Quaternion.identity);
                StartCoroutine(Explosion(playerBomb, fuseTime, layerMask));
                currentBombs--;
                bombUI.UpdateBomb(currentBombs);
            }
            Debug.Log(currentBombs);
        }
        if (currentBombs < maxBombs)
        {
            currentCooldown -= Time.deltaTime;
            cooldownPercent = currentCooldown / maxCooldown;
            bombUI.CooldownBar(cooldownPercent);
            if (currentCooldown <= 0)
            {
                currentCooldown = maxCooldown;
                currentBombs++;
                bombUI.UpdateBomb(currentBombs);
            }
            //Debug.Log("currentCooldown: " + currentCooldown);
        }
        

    }



    public RespawnController respawn;
    public HitpointController hp;
    public float additionalRange = 1.5f;
    private List<Vector3> directions = new List<Vector3>() { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
    IEnumerator Explosion(GameObject obj, float destroyTime, int collisionLayer)
    {
        bool playerHit = false;
        Destroy(obj, destroyTime);
        yield return new WaitForSeconds(destroyTime - 0.1f);
        foreach (Vector3 direction in directions)
        {
            if (!playerHit && Physics.Raycast(obj.transform.position - direction, direction * additionalRange, out explosion, rayLength, collisionLayer) )
            {
                Debug.Log("------------------------------------------");
                Debug.Log("Collided with " + explosion.collider.gameObject.name);
                if (explosion.collider.gameObject.CompareTag("Player"))
                {
                    //if numlives == 0 give game over screen
                    hp.DecLives();
                    StartCoroutine(respawn.Respawn());
                    if (hp.numLives == 0)
                    {
                        Debug.Log("Game Over");
                        uiController.GameOver();
                    }
                    playerHit = true;
                    Debug.Log("Player hit by explosion");
                }
            }
            if (Physics.Raycast(obj.transform.position, direction * additionalRange, out explosion, rayLength, collisionLayer))
            {
                Debug.Log("------------------------------------------");
                Debug.Log("Collided with " + explosion.collider.gameObject.name);
                if (explosion.collider.gameObject.CompareTag("BreakableBlock"))
                {
                    Destroy(explosion.collider.gameObject);
                }
                else if (explosion.collider.gameObject.CompareTag("Player"))
                {
                    //if numlives == 0 give game over screen
                    hp.DecLives();
                    StartCoroutine(respawn.Respawn());
                    if (hp.numLives == 0)
                    {
                        Debug.Log("Game Over");
                        uiController.GameOver();
                    }
                    playerHit = true;
                    Debug.Log("Player hit by explosion");
                }
            }
        }

    }


}


