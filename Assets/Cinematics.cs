using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematics : MonoBehaviour {

    GameObject camera;
    GameObject player;
    GameObject dialogue;

	// Use this for initialization
	void Start () {
        camera = transform.GetChild(0).gameObject;
        player = GameObject.Find("Player");
        dialogue = GameObject.Find("Canvas").transform.GetChild(8).gameObject;
        GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.GetChild(1).gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        switch(MapMovement.GameMode)
        {
            case MapMovement.gameMode.SPEED:
                dialogue.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                break;
            case MapMovement.gameMode.PUZZLE:
                dialogue.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                break;
            case MapMovement.gameMode.SURVIVAL:
                dialogue.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
                break;
        }
        if (Vector3.Distance(player.transform.position, camera.transform.position) > 3 && !Input.GetKeyDown(KeyCode.E))
        {
            Time.timeScale = 0.0f;
            camera.transform.LookAt(player.transform.position);
            camera.transform.position = Vector3.Lerp(camera.transform.position, player.transform.position, 0.0033f);
        }
        else if(Vector3.Distance(player.transform.position, camera.transform.position) < 3 || Input.GetKeyDown(KeyCode.E))
        {
            Time.timeScale = 1.0f;
            dialogue.SetActive(false);
            GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("Canvas").transform.GetChild(1).gameObject.SetActive(true);
            GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(true);
            if (!PlayerPrefs.HasKey("Tuto1")) PlayerPrefs.SetInt("Tuto1", 0);
            if (PlayerPrefs.GetInt("Tuto1") == 0)
            {
                tutorialManager.Instance.activateTutorial(1);
                PlayerPrefs.SetInt("Tuto1", 1);
            }
            Destroy(this.gameObject);
        }
	}
}
