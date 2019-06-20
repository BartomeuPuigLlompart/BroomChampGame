using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trophiesManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (!PlayerPrefs.HasKey("SpeedTrophie")) PlayerPrefs.SetInt("SpeedTrophie", 0);
        if (!PlayerPrefs.HasKey("PuzzleTrophie")) PlayerPrefs.SetInt("PuzzleTrophie", 0);
        if (!PlayerPrefs.HasKey("SurvivalTrophie")) PlayerPrefs.SetInt("SurvivalTrophie", 0);

        transform.GetChild(6).GetChild(0).gameObject.SetActive(PlayerPrefs.GetInt("SpeedTrophie") == 1 ? true : false);
        transform.GetChild(6).GetChild(1).gameObject.SetActive(PlayerPrefs.GetInt("PuzzleTrophie") == 1 ? true : false);
        transform.GetChild(6).GetChild(2).gameObject.SetActive(PlayerPrefs.GetInt("SurvivalTrophie") == 1 ? true : false);
    }
}
