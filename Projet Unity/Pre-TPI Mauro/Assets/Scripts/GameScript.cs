using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameScript : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;
    private int numberEnemies=0;
    private int score = 0;
    private int comboMultipier = 0;
    [SerializeField]
    private GameObject scoreLabel; 
    [SerializeField]
    private GameObject comboLabel;
    [SerializeField]
    private GameObject player;
    private int playerLives;
    [SerializeField]
    private float deathTimer=0f;
    private float deathCounter=0;
    private bool playerAlive=false;
    [SerializeField]
    private GameObject livesLabel;
    private List<GameObject> enemyList; 

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies(2);
        playerLives = 4;
        enemyList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckSpawns();
    }

    void SpawnEnemies(int rows)
    {
        for(int i = 0; i < rows; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                Instantiate(enemy, new Vector3(-7f+1*j, 4f + (-1.5f * i), 0),transform.rotation);
                numberEnemies++;
            }
        }
    }
    public void EnemyKilled()
    {
        numberEnemies--;
    }
    public void AddScore(int value)
    {
        if (value == 100)
        {
            comboMultipier++;
            score = score + comboMultipier * value;
        }
        else
        {
            score = score + value;
        }
        scoreLabel.GetComponent<TextMeshProUGUI>().text = "Current Score : " + score;
        comboLabel.GetComponent<TextMeshProUGUI>().enabled = comboMultipier >= 4;
        comboLabel.GetComponent<TextMeshProUGUI>().text = "Current Score : " + comboMultipier;
    }
    public void ComboBreaker()
    {
        comboMultipier = 0;
        comboLabel.GetComponent<TextMeshProUGUI>().enabled = false;
    }

    public void PlayerKilled()
    {
        deathCounter=0;
        playerAlive=false;
        ComboBreaker();
    }
    private void CheckSpawns()
    {
        if (!playerAlive)
        {
            if (deathCounter > deathTimer)
            {
                SpawnPlayer(0);
            }
            deathCounter+=Time.deltaTime;
        }
    }

    public void SpawnPlayer(int playerId)
    {
        if (playerLives > 0)
        {
            playerLives--;
            livesLabel.GetComponent<TextMeshProUGUI>().text = playerLives + " Lives Remaining";
            if(playerLives==1) livesLabel.GetComponent<TextMeshProUGUI>().text = playerLives + " Life Remaining";
            Instantiate(player, new Vector3(0, -3.5f, 0), transform.rotation);
            playerAlive = true;
            deathTimer = 2f;
        }
    }
    public void ChangeAlienDirection()
    {
        foreach (GameObject alien in enemyList)
        {
            alien.GetComponent<EnemyScript>().Advance();
        }
    }
}
