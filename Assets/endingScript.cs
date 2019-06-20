using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class endingScript : MonoBehaviour {

    GameObject message;
    GameObject canvas;

	// Use this for initialization
	void Start () {
        canvas = GameObject.Find("Canvas");
        message = canvas.transform.GetChild(4).gameObject;
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            message.SetActive(true);
            message.transform.GetChild(1).GetComponent<Text>().text = "You finished " + canvas.transform.GetChild(2).gameObject.GetComponent<Text>().text;
            if (canvas.transform.GetChild(2).gameObject.GetComponent<Text>().text.Substring(0, 1) == "1") {
                switch (SceneManager.GetActiveScene().name)
                {
                    case "speed":
                        PlayerPrefs.SetInt("SpeedTrophie", 1);
                        break;
                    case "puzzle":
                        PlayerPrefs.SetInt("PuzzleTrophie", 1);
                        break;
                    case "survival":
                        PlayerPrefs.SetInt("SurvivalTrophie", 1);
                        break;
                }
                    }
            StartCoroutine(backToMenu(3.0f));
        }
    }

    IEnumerator backToMenu(float time)
    {
        yield return new WaitForSeconds(time);

        if(PlayerPrefs.GetInt("SpeedTrophie") == 1 && PlayerPrefs.GetInt("PuzzleTrophie") == 1 && PlayerPrefs.GetInt("SurvivalTrophie") == 1)
            loadScreen.Instancia.CargarEscena("Credits");
        else
            loadScreen.Instancia.CargarEscena("Principal Menu");
    }
}
