using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsScript : MonoBehaviour
{
    
    public void SliderUpdate()
    {
        AudioListener.volume = GameObject.Find("VolumeSlider").GetComponent<Slider>().value;
        GameScript gameScript = GameObject.FindObjectOfType<GameScript>();
        gameScript.PlaySound(1);
    }
    public void GoBack()
    {
        GameScript gameScript = GameObject.FindObjectOfType<GameScript>();
        gameScript.PlaySound(1);
        this.gameObject.SetActive(false);
        if (gameScript.GameState == 1)
        {
            MainMenuScript[] mainMenuScripts = Resources.FindObjectsOfTypeAll<MainMenuScript>();
            mainMenuScripts[0].gameObject.SetActive(true);
            GameObject.Find("PlayButton").GetComponent<Button>().Select();
        }
        else
        {
            PauseScript[] pauseMenu = Resources.FindObjectsOfTypeAll<PauseScript>();
            pauseMenu[0].gameObject.SetActive(true);
            GameObject.Find("ResumeButton").GetComponent<Button>().Select();
        }

    }
}
