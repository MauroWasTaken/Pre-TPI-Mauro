using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{

    //Global Variables
    /// <summary>
    /// 1: Main menu
    /// 2: Game
    /// 3: Gameover sequence
    /// </summary>
    private int gameState = 1;
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject gameHUD;
    [SerializeField]
    private GameObject scoreBoard;
    [SerializeField]
    private GameObject gameOver;
    [SerializeField]
    private GameObject levelTransition;
    private float transitionTimer;

    // sound Variables
    [SerializeField]
    private AudioClip[] audioClips;
    [SerializeField]
    private AudioSource[] audioSources;

    //Game variables
    [SerializeField]
    private GameObject scoreLabel;
    [SerializeField]
    private GameObject comboLabel;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject livesLabel;
    [SerializeField]
    private GameObject enemy;
    //score variables
    private int score;
    private int comboMultipier;
    //life variables       
    private int playerLives;
    [SerializeField]
    private float deathTime = 0f;
    private float[] deathTimer = { 0, 0, 0 };
    private bool[] playerAlive = { false, false, false };
    private bool isPaused = false;
    private bool nextLevel = false;
    public int nbPlayers = 1;
    //level loading variables
    private int level;
    private bool enemyMad;
    private bool SoloPlay = false;
    public int GameState { get => gameState; }
    public bool IsPaused { get => isPaused; }
    public int Score { get => score; }
    public bool NextLevel { set => nextLevel = value; }
    public bool SoloPlay1 { get => SoloPlay; }


    // Start is called before the first frame update
    void Start()
    {
        ChangeGameState(1);
    }

    // Update is called once per frame
    void Update()
    {

        UpdateGameState();
    }
    public void ChangeGameState(int stateToChange)
    {
        if (isPaused)
        {
            TogglePause();
        }
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject singleObject in allObjects)
        {
            if (singleObject.name.Contains("Player") | singleObject.name.Contains("Enemy") | singleObject.name.Contains("Laser") | singleObject.name.Contains("LaserBall") | singleObject.name.Contains("Explosion"))
            {
                Destroy(singleObject);
            }
            else if (singleObject.name == "ScoreBoard" | singleObject.name == "Options" | singleObject.name == "PauseMenu" | singleObject.name == "GameHUD" | singleObject.name == "MainMenu" | singleObject.name == "GameOver")
            {
                singleObject.SetActive(false);
            }
        }
        /// <summary>
        /// 1: Main menu
        /// 2: Game
        /// 3: Gameover sequence
        /// </summary>
        switch (stateToChange)
        {
            case 1:
                mainMenu.SetActive(true);
                GameObject.Find("PlayButton").GetComponent<Button>().Select();
                AddScore(-score);
                break;
            case 2:
                deathTimer[0] = 0;
                deathTimer[1] = 0;
                deathTimer[2] = 0;
                level = 0;
                comboMultipier = 0;
                AddScore(-score);
                playerLives = 4;
                transitionTimer = 0;
                levelTransition.SetActive(true);
                gameHUD.SetActive(false);
                GameObject.Find("ScoreboardLable").GetComponent<TextMeshProUGUI>().text = "Level 1";
                break;
            case 3:
                gameState = 3;
                transitionTimer = 0;
                gameOver.SetActive(true);
                break;
        }
        gameState = stateToChange;
    }
    private void UpdateGameState()
    {
        /// <summary>
        /// 1: Main menu
        /// 2: Game
        /// 3: Gameover sequence
        /// </summary>
        switch (gameState)
        {
            case 1:

                break;
            case 2:
                if (transitionTimer > 2 & !gameHUD.activeSelf)
                {

                    gameHUD.SetActive(true);
                    levelTransition.SetActive(false);
                    if (nbPlayers == 2)
                    {
                        if (deathTimer[1] >= 0) SpawnPlayer(1);
                        if (deathTimer[2] >= 0) SpawnPlayer(2);
                    }
                    else
                    {
                        SpawnPlayer(0);
                    }

                    LevelStart();
                }
                if (gameHUD.activeSelf)
                {
                    CheckSpawns();
                    if (Input.GetButtonDown("Cancel"))
                    {
                        TogglePause();
                    }
                    if (nextLevel)
                    {
                        level++;
                        gameHUD.SetActive(false);

                        foreach (PlayerScript playerObject in GameObject.FindObjectsOfType<PlayerScript>())
                        {
                            Destroy(playerObject.gameObject);
                        }
                        playerLives++;
                        levelTransition.SetActive(true);
                        GameObject.Find("ScoreboardLable").GetComponent<TextMeshProUGUI>().text = "Level " + (level + 1) + "";
                        if (level % 5 == 0)
                        {
                            GameObject.Find("ExtraMessageLabel").GetComponent<TextMeshProUGUI>().text = "The enemies grow stronger";
                        }
                        else
                        {
                            GameObject.Find("ExtraMessageLabel").GetComponent<TextMeshProUGUI>().text = "";
                        }
                        transitionTimer = 0;
                        nextLevel = false;
                    }
                }
                break;
            case 3:
                if (!scoreBoard.activeSelf & transitionTimer > 3)
                {
                    gameOver.SetActive(false);
                    scoreBoard.SetActive(true);
                }
                break;
            default:
                ChangeGameState(1);
                break;
        }
        transitionTimer += Time.deltaTime;
    }
    void LevelStart()
    {
        switch (level % 5)
        {
            case 0:
                enemyMad = false;
                SpawnEnemies(1);
                break;
            case 1:
                enemyMad = false;
                SpawnEnemies(2);
                break;
            case 2:
                enemyMad = true;
                SpawnEnemies(1);
                break;
            case 3:
                enemyMad = true;
                SpawnEnemies(2);
                break;
            case 4:
                enemyMad = true;
                SpawnEnemies(3);
                break;
        }
    }
    void SpawnEnemies(int rows)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                GameObject enemyObject = Instantiate(enemy, new Vector3(-7f + 1 * j, 4f + (-1.5f * i), 0), transform.rotation);
                enemyObject.GetComponent<EnemyScript>().isMad = true;
            }
        }
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
        comboLabel.GetComponent<TextMeshProUGUI>().text = "Current Combo : " + comboMultipier;
    }
    public void ComboBreaker()
    {
        comboMultipier = 0;
        comboLabel.GetComponent<TextMeshProUGUI>().enabled = false;
    }

    public void PlayerKilled(int playerId)
    {
        PlaySound(2);
        deathTimer[playerId] = 0;
        playerAlive[playerId] = false;
        ComboBreaker();
        bool playersStillAlive = false;
        foreach (bool playerbool in playerAlive)
        {
            if (playerbool)
            {
                playersStillAlive = true;
            }
        }
        if (playerLives == 0 & playersStillAlive)
        {
            deathTimer[playerId] = -1;
        }
        if (playerLives == 0 & !playersStillAlive)
        {
            ChangeGameState(3);
        }
    }
    private void CheckSpawns()
    {
        if (nbPlayers == 2)
        {
            for (int i = 1; i < 3; i++)
            {
                if (!playerAlive[i])
                {
                    if (deathTimer[i] > deathTime)
                    {
                        SpawnPlayer(i);
                    }
                    if(deathTimer[i]>=0)deathTimer[i] += Time.deltaTime;
                }
            }
        }
        else
        {
            if (!playerAlive[0])
            {
                if (deathTimer[0] > deathTime)
                {
                    SpawnPlayer(0);
                }
                deathTimer[0] += Time.deltaTime;
            }
        }


    }

    public void SpawnPlayer(int playerId)
    {
        if (playerLives > 0)
        {
            playerLives--;
            livesLabel.GetComponent<TextMeshProUGUI>().text = playerLives + " Lives Remaining";
            if (playerLives == 1) livesLabel.GetComponent<TextMeshProUGUI>().text = playerLives + " Life Remaining";
            GameObject spawnedPlayer = Instantiate(player, new Vector3(0, -3.5f, 0), transform.rotation);
            spawnedPlayer.GetComponent<PlayerScript>().playerId = playerId;
            playerAlive[playerId] = true;
            deathTime = 0.75f;
        }
    }
    public void TogglePause()
    {
        PauseScript[] pause = Resources.FindObjectsOfTypeAll<PauseScript>();
        if (isPaused)
        {
            if (pause[0].gameObject.activeSelf)
            {
                Time.timeScale = 1f;
                pause[0].gameObject.SetActive(false);
                isPaused = false;
            }
        }
        else
        {
            Time.timeScale = 0f;
            pause[0].gameObject.SetActive(true);
            GameObject.Find("ResumeButton").GetComponent<Button>().Select();
            isPaused = true;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="soundID">0-Lazer sound,1-Menu sound,2-player Death, 3- Enemy death</param>
    public void PlaySound(int soundID)
    {
        audioSources[soundID].clip = audioClips[soundID];
        audioSources[soundID].Play();
    }
}
