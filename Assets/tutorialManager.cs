using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialManager : MonoBehaviour {

    public static tutorialManager Instance;

    private GameObject tutorial;

	// Use this for initialization
	void Start () {
        Instance = this;
        tutorial = transform.GetChild(0).gameObject;
        tutorial.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if(tutorial.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            tutorial.SetActive(false);
            for(int i = 1; i < transform.childCount; i++)
            {
                if(transform.GetChild(i).gameObject.activeSelf)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                    break;
                }
            }
            Time.timeScale = 1.0f;
        }
	}

    public void activateTutorial(int idx)
    {
        tutorial.SetActive(true);
        transform.GetChild(idx).gameObject.SetActive(true);
        Time.timeScale = 0.0f;
    }
}
