using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{

    [Header("UI")] 
    public UIController ui;
    public int maxBombs = 2;
    public float maxCooldown = 3.0f;
    public int numBombs;
    private float currentCooldown;

    [Header("Bomb Rendering")]
    public GameObject prefabBomb;
    public GameObject playerObj;
    private GameObject playerBomb;
    private Player player1;

    [Header("Bomb Explosion")]
    public float fuseTime = 2.5f;
    public int layerNum = 7;
    public float explosionLength = 1.0f;
    public float additionalRange = 1.5f;
    private int layerMask;
    private List<Vector3> directions = new List<Vector3>() { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
    private RaycastHit explosion;


    
    
    // Start is called before the first frame update
    void Start()
    {
        player1 = playerObj.GetComponent<Player>();
        numBombs = maxBombs;
        layerMask = 1 << layerNum;
    }

    // Update is called once per frame
    void Update()
    {
        if (numBombs < maxBombs)
        {
            currentCooldown -= Time.deltaTime;
            ui.CooldownBar(currentCooldown / maxCooldown);
            if (currentCooldown <= 0)
            {
                currentCooldown = maxCooldown;
                numBombs++;
                ui.UpdateBomb(numBombs);
            }
            //Debug.Log("currentCooldown: " + currentCooldown);
        }
    }

    public void DeployBomb()
    {
        if (numBombs > 0)
        {   
            playerBomb = Instantiate(prefabBomb, new Vector3(Mathf.Round(playerObj.transform.position.x), 1, Mathf.Round(playerObj.transform.position.z)), Quaternion.identity);
            StartCoroutine(Explosion(playerBomb, fuseTime, layerMask));
            numBombs--;
            ui.UpdateBomb(numBombs);
            Debug.Log("Bomb Deployed");
        }
    }

    IEnumerator Explosion(GameObject obj, float destroyTime, int collisionLayer)
    {
        bool playerHit = false;
        Destroy(obj, destroyTime);
        yield return new WaitForSeconds(destroyTime - 0.1f);
        Debug.Log("Explosion");
        foreach(Vector3 direction in directions)
        {
            if (!playerHit && Physics.Raycast(obj.transform.position - direction, direction * additionalRange, out explosion, explosionLength, collisionLayer) )
            {
               if (explosion.collider.gameObject.CompareTag("Player"))
               {
                    player1.DecLives();
                    StartCoroutine(player1.Respawn());
                    if (player1.numLives == 0)
                    {
                        ui.GameOver();
                        Debug.Log("Game Over");
                    }
                    playerHit = true;
                    Debug.Log("Player hit");
               } 
            }
            if (Physics.Raycast(obj.transform.position, direction * additionalRange, out explosion, explosionLength, collisionLayer))
            {
                if (explosion.collider.gameObject.CompareTag("BreakableBlock"))
                {
                    Destroy(explosion.collider.gameObject);
                } 
                else if (explosion.collider.gameObject.CompareTag("Player"))
                {
                    player1.DecLives();
                    StartCoroutine(player1.Respawn());
                    if (player1.numLives == 0)
                    {
                        ui.GameOver();
                        Debug.Log("Game Over");
                    }
                    playerHit = true;
                    Debug.Log("Player hit");
                } 
            }
        }
    }

}
