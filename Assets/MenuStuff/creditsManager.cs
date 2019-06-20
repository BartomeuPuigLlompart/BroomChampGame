using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creditsManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.GetInt("SpeedTrophie") == 1 && PlayerPrefs.GetInt("PuzzleTrophie") == 1 && PlayerPrefs.GetInt("SurvivalTrophie") == 1)
            GameObject.Find("Canvas").transform.GetChild(1).gameObject.SetActive(true);
            StartCoroutine(endCredits(18.0f));
	}

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) loadScreen.Instancia.CargarEscena("Principal Menu");
    }

    IEnumerator endCredits(float time)
    {
        yield return new WaitForSeconds(time);

        loadScreen.Instancia.CargarEscena("Principal Menu");
    }
}
