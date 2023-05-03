using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public int maxLives = 3;
    public int numLives;
    public float movementSpeed = 10f;

    public int numKeys = 0;

    public UIController ui;
    private Vector3 currentPosition;

    [Header("Respawn")]
    public float respawnTime = 2.0f;
    public float addHeight = 50;
    public Vector3 respawnPoint;
    public BombController bc;

    [Header("Audio")]
    public AudioSource[] sounds;
    private AudioSource deathSound;
    private AudioSource pickupSound;
    private AudioSource reviveSound;
    

    // Start is called before the first frame update
    void Start()
    {
        numLives = maxLives; 
        ui.UpdateLives();  

        sounds = GetComponents<AudioSource>();
        deathSound = sounds[0];
        pickupSound = sounds[1];
        reviveSound = sounds[2];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move(Vector3 newPos)
    {
        transform.position = transform.position  + (newPos * movementSpeed);
    }
    public void DecLives()
    {
        deathSound.Play();
        --numLives;
        ui.UpdateLives();
    }

    public void ResetLives()
    {
        numLives = maxLives;
        ui.UpdateLives();
    }

    public IEnumerator Respawn()
    {
        transform.Translate(new Vector3(transform.position.x, transform.position.y + addHeight, transform.position.z));
        yield return new WaitForSeconds(respawnTime);
        transform.position = respawnPoint;
        reviveSound.Play();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("collided with enemy");
            bc.KillPlayer();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Key"))
        {
            pickupSound.Play();
            numKeys++;
            col.gameObject.SetActive(false);
            ui.UpdateKeys();
            Debug.Log("picked up key");
        }
    }

}
