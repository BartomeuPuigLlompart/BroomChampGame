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
            StartCoroutine(backToMenu(3.0f));
        }
    }

    IEnumerator backToMenu(float time)
    {
        yield return new WaitForSeconds(time);

        SceneManager.LoadScene("Principal Menu");
    }
}
