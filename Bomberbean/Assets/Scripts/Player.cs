using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public int maxLives = 3;
    public int numLives;
    public float movementSpeed = 10f;
    public UIController ui;
    private Vector3 currentPosition;

    [Header("Respawn")]
    public float respawnTime = 2.0f;
    public float addHeight = 50;
    public Vector3 respawnPoint;
    



    // Start is called before the first frame update
    void Start()
    {
        numLives = maxLives; 
        ui.UpdatePlayer();  
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
        --numLives;
        ui.UpdatePlayer();
    }

    public void ResetLives()
    {
        numLives = maxLives;
        ui.UpdatePlayer();
    }

    public IEnumerator Respawn()
    {
        transform.Translate(new Vector3(transform.position.x, transform.position.y + addHeight, transform.position.z));
        yield return new WaitForSeconds(respawnTime);
        transform.position = respawnPoint;
    }

}
