using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionRenderer : MonoBehaviour
{
    public GameObject explosionPrefab;
    public float lifeTime = 0.9f;
    private GameObject explosionRend;
    private List<Vector3> directions = new List<Vector3>() {Vector3.zero, Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayExplosion()
    {
        foreach (Vector3 direction in directions)
        {
            explosionRend = Instantiate(explosionPrefab, new Vector3(Mathf.Round(transform.position.x), 1, Mathf.Round(transform.position.z)) + direction, Quaternion.identity);
            explosionRend.SetActive(true);
            Destroy(explosionRend, lifeTime);
            Debug.Log(direction);
        }     
        Debug.Log("PlayExplosion called");

    }
}
