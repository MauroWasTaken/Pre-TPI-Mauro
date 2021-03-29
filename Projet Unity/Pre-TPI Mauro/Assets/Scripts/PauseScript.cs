using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// classe qui gere le menu pause
/// </summary>
public class PauseScript : MonoBehaviour
{
    /// <summary>
    /// permets de retourner dans la partie en cours
    /// </summary>
    public void ResumeGame()
    {
        GameScript gameScript = UnityEngine.Object.FindObjectOfType<GameScript>();
        gameScript.PlaySound(1);
        gameScript.TogglePause();
    }
    /// <summary>
    /// permets de recommencer une partie
    /// </summary>
    public void RestartGame()
    {
        GameScript gameScript = UnityEngine.Object.FindObjectOfType<GameScript>();
        gameScript.PlaySound(1);
        gameScript.ChangeGameState(2);
    }
    /// <summary>
    /// permets d'ouvrir le menu options
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
    /// permets retourner dans le menu
    /// </summary>
    public void CloseGame()
    {
        GameScript gameScript = UnityEngine.Object.FindObjectOfType<GameScript>();
        gameScript.PlaySound(1);
        gameScript.ChangeGameState(1);
    }
}
