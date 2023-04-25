using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HitpointController : MonoBehaviour
{
    public int maxLives = 3;
    public int numLives;
    private TextMeshProUGUI livesUI;
    
    // Start is called before the first frame update
    void Start()
    {
        livesUI = GetComponentInChildren<TextMeshProUGUI>();
        numLives = maxLives;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ChangeLivesCount(--numLives);
        }
    }

    public void ChangeLivesCount(int newCount)
    {
        numLives = newCount;
        livesUI.text = "Lives: " + numLives;
    }

    public void DecLives()
    {
        --numLives;
        livesUI.text = "Lives: " + numLives;
    }

    public void ResetLives()
    {
        numLives = maxLives;
        livesUI.text = "Lives: " + numLives;
    }


}
