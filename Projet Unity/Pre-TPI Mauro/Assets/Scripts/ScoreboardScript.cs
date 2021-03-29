using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using TMPro;
using UnityEngine.UI;
using System;
/// <summary>
/// classe qui gere le scoreboard
/// </summary>
public class ScoreboardScript : MonoBehaviour
{
    [SerializeField]
    private GameObject NewRecordHUD;
    private GameScript gameScript;
    private List<int> scoresValues;
    private List<string> scoresNames;
    private int finalScore;
    /// <summary>
    /// Fonction qui se lance quand le scoreboard devient actif
    /// </summary>
    private void OnEnable()
    {
        gameScript = GameObject.FindObjectOfType<GameScript>();
        GameObject.Find("GoBackButton").GetComponent<Button>().Select();
        GetScores();
        CheckWorldRecord();
        
    }
    /// <summary>
    /// Check si le score est dans le top 8
    /// </summary>
    private void CheckWorldRecord()
    {
        if (gameScript.GameState == 3)
        {
            foreach(int value in scoresValues)
            {
                if (value<gameScript.Score)
                {
                    NewRecordHUD.SetActive(true);
                    finalScore = gameScript.Score;
                    GameObject.Find("RunScoreLabel").GetComponent<TextMeshProUGUI>().text = gameScript.Score+" -";
                    GameObject.Find("NewRecordText").GetComponent<TMP_InputField>().Select();
                    gameScript.PlaySound(1);
                }
            }
        }
        
    }
    /// <summary>
    /// Enregistre les scores 
    /// </summary>
    public void SaveWorldRecord()
    {
        if (GameObject.Find("RunText").GetComponent<TextMeshProUGUI>().text.Length > 1)
        {
            List<int> newScoresValues = new List<int>();
            List<string> newScoresNames = new List<string>();

            bool alreadySaved = false;
            using (StreamWriter sw = new StreamWriter("Scores.CSV"))
            {
                for (int i = 0; i < scoresValues.Count - 1; i++)
                {
                    if (newScoresValues.Count < 8)
                    {
                        if (scoresValues[i] < finalScore)
                        {
                            if (!alreadySaved)
                            {
                                newScoresValues.Add(finalScore);
                                newScoresNames.Add(GameObject.Find("RunText").GetComponent<TextMeshProUGUI>().text);
                                sw.WriteLine(finalScore + "," + GameObject.Find("RunText").GetComponent<TextMeshProUGUI>().text);
                                alreadySaved = true;
                            }
                        }
                        sw.WriteLine(scoresValues[i] + "," + scoresNames[i]);
                        newScoresValues.Add(scoresValues[i]);
                        newScoresNames.Add(scoresNames[i]);
                    }
                }
            }
            GameObject.Find("NewRecord").SetActive(false);
            GoBack();
        }
    }
    /// <summary>
    /// fonction unity appellée à chaque fois que il y a un changement dans le gui
    /// Check si le joueur à appuyé enter apres avour tappé sont pseudo
    /// </summary>
    private void OnGUI()
    {
        if (Event.current.isKey && Event.current.type == EventType.KeyDown)
        {
            if (EventSystem.current.currentSelectedGameObject == GameObject.Find("NewRecordText"))
            {
                if (Event.current.keyCode == KeyCode.Return)
                {
                    SaveWorldRecord();
                }
            }
        }
    }
    /// <summary>
    /// Va chercher les scores dans un fichier CSV
    /// </summary>
    void GetScores()
    {
        scoresValues = new List<int>();
        scoresNames = new List<string>();
        if (File.Exists("Scores.CSV"))
        {
            string data = File.ReadAllText("Scores.CSV");
            string[] lines = data.Split("\n"[0]);
            foreach (string line in lines)
            {
                string[] lineData = line.Trim().Split(","[0]);
                if (lineData.Length != 1)
                {
                    scoresValues.Add(int.Parse(lineData[0]));
                    scoresNames.Add(lineData[1]);
                }
            }
        }
        else
        {
            scoresValues.Add(99999);
            scoresNames.Add("MSS");
            scoresValues.Add(50000);
            scoresNames.Add("MSS");
            scoresValues.Add(20000);
            scoresNames.Add("MSS");
            scoresValues.Add(10000);
            scoresNames.Add("MSS");
            scoresValues.Add(5000);
            scoresNames.Add("MSS");
            scoresValues.Add(1000);
            scoresNames.Add("MSS");
            scoresValues.Add(350);
            scoresNames.Add("MSS");
            scoresValues.Add(100);
            scoresNames.Add("MSS");

        }
        GameObject.Find("Top3Label").GetComponent<TextMeshProUGUI>().text = "";
        GameObject.Find("Top8Label").GetComponent<TextMeshProUGUI>().text = "";
        for (int i = 0; i < scoresValues.Count; i++)
        {
            if (i < 3)
            {
                GameObject.Find("Top3Label").GetComponent<TextMeshProUGUI>().text = GameObject.Find("Top3Label").GetComponent<TextMeshProUGUI>().text + scoresValues[i] + " - " + scoresNames[i] + "\n";
            }
            else
            {
                GameObject.Find("Top8Label").GetComponent<TextMeshProUGUI>().text = GameObject.Find("Top8Label").GetComponent<TextMeshProUGUI>().text + scoresValues[i] + " - " + scoresNames[i] + "\n";
            }
        }                   
    }
    /// <summary>
    /// Retourne dans le menu avec les bons paramettres
    /// </summary>
    public void GoBack()
    {
        gameScript.ChangeGameState(1);
        gameScript.PlaySound(1);
    }
}
