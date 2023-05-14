using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIController : MonoBehaviour
{

    [Header("UI Controller")]
    public GameObject pauseMenu;
    public GameObject bombIndicators;
    public GameObject gameOver;
    public bool isPaused;

    [Header("Bomb UI")]
    public BombController bc;
    //public TextMeshProUGUI count;
    public Image[] bombIcons;
    public float maxWidth = 500;
    public Image cooldownBar;
    private RectTransform barSize;

    [Header ("Player UI")]
    public Player player1;
    
    public Image[] heartIcons;

    [Header ("Keys UI")]
    
    public Image[] keyIcons;
    public TextMeshProUGUI keysUI;
    public TextMeshProUGUI instructionText;

    [Header ("Timer UI")]
    float currentTime;
    public TextMeshProUGUI currentTimeText;

    [Header ("Game Win")]
    public GameObject winScreen;
    public int totalBombsUsed = 0;
    private float finalTime;
    private int finalKeys;
    public TextMeshProUGUI stats;

    [Header ("Audio")]
    public AudioSource loseSound;
    public AudioSource winSound;

    // Start is called before the first frame update
    void Start()
    {
        InitIcons();
        pauseMenu.SetActive(false);
        gameOver.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
        barSize = cooldownBar.rectTransform;
        CooldownBar(0);

        currentTime = 0;
    }
    
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                UnpauseGame();
            else   
                PauseGame();
        }
        currentTime += Time.deltaTime;
        
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.Minutes.ToString("D2") + ":" + time.Seconds.ToString("D2");

        
       
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        bombIndicators.SetActive(false);
        isPaused = true;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        bombIndicators.SetActive(true);
        isPaused = false;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOver.SetActive(true);
        loseSound.Play();
    }

    public void GameWin()
    {
        Time.timeScale = 0;
        finalTime = currentTime;
        finalKeys = player1.numKeys;
        winScreen.SetActive(true);
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        stats.text = "Finishing Time: " + time.Minutes.ToString("D2") + ":" + time.Seconds.ToString("D2") + "\n";
        stats.text += "Number of Keys Collected: " +  finalKeys + "\n";
        stats.text += "Number of Bombs Used: " + totalBombsUsed;
        winSound.Play();
    }

     public void CooldownBar(float percentOfSize)
    {
        barSize.sizeDelta = new Vector2(maxWidth * percentOfSize, barSize.sizeDelta.y);
    }

    private void InitIcons()
    {
        UpdateBomb();
        UpdateLives();
        UpdateKeys();
    }

    public void UpdateBomb()
    {
        //count.text = "" + newCount;
        // Enables bomb icons in accordance to available bombs to the player
        for (int i = 0; i < bombIcons.Length; ++i)
            bombIcons[i].enabled = (i < bc.numBombs);

        Debug.Log("numBombs: " + bc.numBombs);
    }

    public void UpdateLives()
    {
        //livesUI.text = "Lives: " + player1.numLives;  
        // Enables heart icons in accordance to amount of lives available to the player
        for (int i = 0; i < heartIcons.Length; ++i)
            heartIcons[i].enabled = (i < player1.numLives);
        

    }

    public void UpdateKeys()
    {
        //keysUI.text = "Keys: " + player1.numKeys;
        // Enables key icons in accordance to the amount of keys the player has
        Debug.Log("in UpdateKeys");
        for (int i = 0; i < keyIcons.Length; i++)
            keyIcons[i].enabled = (i < player1.numKeys);
        
        if (player1.numKeys == 3)
            instructionText.text = "door unlocked!";

    }

}
