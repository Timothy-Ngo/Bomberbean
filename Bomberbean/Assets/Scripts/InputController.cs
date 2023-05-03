using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public BombController bc;
    public GameObject playerObj;
    private Player player1;
    

    // Start is called before the first frame update
    void Start()
    {
        player1 = playerObj.GetComponent<Player>();
    }

    
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            bc.DeployBomb();   
        }
         
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D)) 
        {
            player1.Move(Vector3.right * Time.deltaTime); 
             
        }

        if (Input.GetKey(KeyCode.A)) 
        {
            player1.Move(Vector3.left * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W)) 
        {
            player1.Move(Vector3.forward * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S)) 
        {
            player1.Move(Vector3.back * Time.deltaTime);
        }

    }

    
}
