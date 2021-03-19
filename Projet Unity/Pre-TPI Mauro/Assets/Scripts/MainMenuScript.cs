using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuScript : MonoBehaviour
{
    public void PlayGame()
    {
        GameScript gameScript = UnityEngine.Object.FindObjectOfType<GameScript>();
        gameScript.PlaySound(1);
        gameScript.ChangeGameState(2);
    }
    public void OpenOptions()
    {
        GameScript gameScript = UnityEngine.Object.FindObjectOfType<GameScript>();
        gameScript.PlaySound(1);
        OptionsScript[] options = Resources.FindObjectsOfTypeAll<OptionsScript>();
        options[0].gameObject.SetActive(true);
        GameObject.Find("GoBackButton").GetComponent<Button>().Select();
        this.gameObject.SetActive(false);

    }
    public void OpenScoreBoard()
    {
        GameScript gameScript = UnityEngine.Object.FindObjectOfType<GameScript>();
        gameScript.PlaySound(1);
        ScoreboardScript[] options = Resources.FindObjectsOfTypeAll<ScoreboardScript>();
        options[0].gameObject.SetActive(true);
        GameObject.Find("GoBackButton").GetComponent<Button>().Select();
        this.gameObject.SetActive(false);

    }
    public void CloseGame()
    {
        GameScript gameScript = UnityEngine.Object.FindObjectOfType<GameScript>();
        gameScript.PlaySound(1);
        Application.Quit();
    }
}
