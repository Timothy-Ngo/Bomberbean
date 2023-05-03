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
    private float currentCooldown = 0;

    [Header("Bomb Rendering")]
    public GameObject prefabBomb;
    public GameObject playerObj;
    private GameObject playerBomb;
    private Player player1;

    [Header("Bomb Explosion")]
    public float fuseTime = 2.5f;
    public int layerNum = 7;
    public float explosionLength = 1.0f;
    public float additionalRange = 2f;
    private int layerMask;
    private List<Vector3> directions = new List<Vector3>() { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
    private RaycastHit explosion;

    [Header("Keys")]
    public GameObject prefabKey;
    private GameObject playerKey;

    [Header("Audio")]
    public AudioSource[] sounds;
    public AudioSource bombPutDown;
    public AudioSource bombExplosion;

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
                ui.UpdateBomb();
            }
            //Debug.Log("currentCooldown: " + currentCooldown);
        }
    }

    public void DeployBomb()
    {
        if (numBombs > 0)
        {

            playerBomb = Instantiate(prefabBomb, new Vector3(Mathf.Round(playerObj.transform.position.x) , 1, Mathf.Round(playerObj.transform.position.z)), Quaternion.identity);
            sounds = playerBomb.GetComponents<AudioSource>();
            bombPutDown = sounds[0];
            bombExplosion = sounds[1];
            bombPutDown.Play();
            StartCoroutine(Explosion(playerBomb, fuseTime, layerMask));
            numBombs--;
            ui.UpdateBomb();
            ui.totalBombsUsed++;
            currentCooldown = maxCooldown;
            Debug.Log("Bomb Deployed");
        }
    }

    IEnumerator Explosion(GameObject obj, float destroyTime, int collisionLayer)
    {

        bool playerHit = false;
        Destroy(obj, destroyTime);
        yield return new WaitForSeconds(destroyTime - 1);
        bombExplosion.Play();
        obj.GetComponent<ExplosionRenderer>().PlayExplosion();
        obj.GetComponentInChildren<MeshRenderer>().enabled = false;
        Debug.Log("Explosion");
        foreach (Vector3 direction in directions)
        {
            if (!playerHit && Physics.Raycast(obj.transform.position - direction, direction * additionalRange, out explosion, explosionLength, collisionLayer))
            {
                Debug.Log("Something hit");
                if (explosion.collider.gameObject.CompareTag("Player"))
                {
                    KillPlayer();
                    playerHit = true;
                    Debug.Log("Player hit");
                }
                else if (explosion.collider.gameObject.CompareTag("Enemy"))
                {
                    playerKey = Instantiate(prefabKey, new Vector3(Mathf.Round(explosion.collider.gameObject.transform.position.x), 1.25f, Mathf.Round(explosion.collider.gameObject.transform.position.z)), Quaternion.identity);
                    Destroy(explosion.collider.gameObject);
                    Debug.Log("hit enemy");
                }
            }
            if (Physics.Raycast(obj.transform.position, direction * additionalRange, out explosion, explosionLength, collisionLayer))
            {
                Debug.Log("Something else hit ");
                if (explosion.collider.gameObject.CompareTag("BreakableBlock"))
                {
                    Destroy(explosion.collider.gameObject);
                }
                else if (explosion.collider.gameObject.CompareTag("Enemy"))
                {
                    playerKey = Instantiate(prefabKey, new Vector3(Mathf.Round(explosion.collider.gameObject.transform.position.x), 1.25f, Mathf.Round(explosion.collider.gameObject.transform.position.z)), Quaternion.identity);
                    Destroy(explosion.collider.gameObject);
                    Debug.Log("hit enemy");
                }
                else if (explosion.collider.gameObject.CompareTag("Player"))
                {
                    KillPlayer();
                    playerHit = true;
                    Debug.Log("Player hit");
                }
            }
        }

    }

    public void KillPlayer()
    {
        player1.DecLives();
        StartCoroutine(player1.Respawn());
        if (player1.numLives == 0)
        {
            ui.GameOver();
            Debug.Log("Game Over");
        }
    }

}
