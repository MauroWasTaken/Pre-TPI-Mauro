using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// Classe qui gere le menu options
/// </summary>
public class OptionsScript : MonoBehaviour
{
    [SerializeField]
    Button bulletTypeButton;
    /// <summary>
    /// Fonction unity appelée à chaque image
    /// Change de layout do menu en fonction de l'etat du jeu
    /// </summary>
    void Update()
    {
        GameScript gameScript = GameObject.FindObjectOfType<GameScript>();
        if (gameScript.GameState == 1)
        {
            bulletTypeButton.gameObject.SetActive(true);
        }
        else
        {
            bulletTypeButton.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// Fonction unity appelée quand la valeur du slider est changée
    /// </summary>
    public void SliderUpdate()
    {
        AudioListener.volume = GameObject.Find("VolumeSlider").GetComponent<Slider>().value;
        GameScript gameScript = GameObject.FindObjectOfType<GameScript>();
        gameScript.PlaySound(1);
    }
    /// <summary>
    /// fonction que change le type de balle selectionée
    /// </summary>
    public void ChangeBulletType()
    {
        GameScript gameScript = GameObject.FindObjectOfType<GameScript>();
        gameScript.PlaySound(1);
        if (gameScript.laserType == 2) gameScript.laserType = -1;
        gameScript.laserType++;
        string buttonName = "";
        switch (gameScript.laserType)
        {
            case 0:
                buttonName = "Normal Mode";
                break;
            case 1:
                buttonName = "Big Bullet Mode";
                break;
            case 2:
                buttonName = "MachineGun Mode";
                break;
        }
        GameObject.Find("BulletTypeLabel").GetComponent<TextMeshProUGUI>().text=buttonName;
    }
    /// <summary>
    /// fonction que nous fait retourner dans le menu précédent
    /// </summary>
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
