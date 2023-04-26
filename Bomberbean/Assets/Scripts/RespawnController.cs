using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{

    //DO NOT NEED THIS ANYMORE
    // Start is called before the first frame update
    void Start()
    {
        //playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //private PlayerController playerController;
    public float respawnTime = 2.0f;
    public float addHeight = 50;
    public Vector3 respawnPoint;
    public IEnumerator Respawn()
    {
        //playerController.enabled = false;
        transform.Translate(new Vector3(transform.position.x, transform.position.y + addHeight, transform.position.z));
        yield return new WaitForSeconds(respawnTime);
        //playerController.enabled = true;
        transform.position = respawnPoint;
    }



}
