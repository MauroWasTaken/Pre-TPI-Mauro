using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// classe qui gere le menu principal du jeu
/// </summary>
public class MainMenuScript : MonoBehaviour
{
    /// <summary>
    /// fonction que lance le jeu 
    /// </summary>
    public void PlayGame()
    {
        GameScript gameScript = UnityEngine.Object.FindObjectOfType<GameScript>();
        gameScript.PlaySound(1);
        gameScript.nbPlayers = 1;
        gameScript.ChangeGameState(2);
    }
    /// <summary>
    /// fonction que lance le jeu en mode coop
    /// </summary>
    public void PlayGameCoop()
    {
        GameScript gameScript = UnityEngine.Object.FindObjectOfType<GameScript>();
        gameScript.PlaySound(1);
        gameScript.nbPlayers = 2;
        gameScript.ChangeGameState(2);
        
    }
    /// <summary>
    /// fonction permettant d'ouvrir le menu options
    /// </summary>
    public void OpenOptions()
    {
        GameScript gameScript = UnityEngine.Object.FindObjectOfType<GameScript>();
        gameScript.PlaySound(1);
        OptionsScript[] options = Resources.FindObjectsOfTypeAll<OptionsScript>();
        options[0].gameObject.SetActive(true);
        GameObject.Find("GoBackButton").GetComponent<Button>().Select();
        this.gameObject.SetActive(false);

    }
    /// <summary>
    /// fonction permettant d'ouvrir le scoreboard
    /// </summary>
    public void OpenScoreBoard()
    {
        GameScript gameScript = UnityEngine.Object.FindObjectOfType<GameScript>();
        gameScript.PlaySound(1);
        ScoreboardScript[] options = Resources.FindObjectsOfTypeAll<ScoreboardScript>();
        options[0].gameObject.SetActive(true);
        GameObject.Find("GoBackButton").GetComponent<Button>().Select();
        this.gameObject.SetActive(false);

    }
    /// <summary>
    /// fonction permettant de quitter le jeu
    /// </summary>
    public void CloseGame()
    {
        GameScript gameScript = UnityEngine.Object.FindObjectOfType<GameScript>();
        gameScript.PlaySound(1);
        Application.Quit();
    }
}
