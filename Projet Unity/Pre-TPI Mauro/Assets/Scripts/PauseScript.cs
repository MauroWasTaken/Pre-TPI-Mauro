using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    
    public void ResumeGame()
    {
        GameScript gameScript = UnityEngine.Object.FindObjectOfType<GameScript>();
        gameScript.PlaySound(1);
        gameScript.TogglePause();
    }
    public void RestartGame()
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
    public void CloseGame()
    {
        GameScript gameScript = UnityEngine.Object.FindObjectOfType<GameScript>();
        gameScript.PlaySound(1);
        gameScript.ChangeGameState(1);
    }
}
