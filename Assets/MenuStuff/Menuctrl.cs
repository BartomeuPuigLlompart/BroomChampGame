using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menuctrl : MonoBehaviour {

    public GameObject Principal_Menu;
    public GameObject Options_Menu;
    public GameObject Chapters_Menu;

    Resolution[] resolutions;
    string[] names;

    void Start()
    {
        resolutions = new Resolution[3];
        resolutions[0].width = 1920;
        resolutions[0].height = 1080;
        resolutions[1].width = 1280;
        resolutions[1].height = 720;
        resolutions[2].width = 852;
        resolutions[2].height = 480;

        string[] names = QualitySettings.names;
    }

    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1.0f;
        //SceneManager.LoadScene(sceneName);
        loadScreen.Instancia.CargarEscena(sceneName);
    }

    public void showOptions()
    {
        Principal_Menu.SetActive(false);
        Chapters_Menu.SetActive(false);
        Options_Menu.SetActive(true);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex, true);
    }

    public void showPrincipalMenu()
    {
        Principal_Menu.SetActive(true);
        Options_Menu.SetActive(false);
        Chapters_Menu.SetActive(false);
    }

    public void showChapterMenu()
    {
        Principal_Menu.SetActive(false);
        Options_Menu.SetActive(false);
        Chapters_Menu.SetActive(true);
    }

    public void ExitGameBtn()
    {
        Application.Quit();
    }

    public void resumeGame()
    {
        Time.timeScale = 1.0f;
        GameObject.Find("Canvas").transform.GetChild(6).gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void fullScreenOff()
    {
        if (Screen.fullScreen == true)
        {
            Screen.fullScreen = false;
        }
        else
        {
            Screen.fullScreen = true;
        }
    }

    public void Mute()
    {
        AudioListener.pause = !AudioListener.pause;
    }

    public void SetVolume(float volume)
    {
        audioManager.AudioManager.audioSource.volume = volume;

    }

}
