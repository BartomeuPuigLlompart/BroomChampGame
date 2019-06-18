using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splashScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (Time.realtimeSinceStartup > 10.0f)
        {
            GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(true);
            Destroy(this.gameObject);
        }

        StartCoroutine(stopSplash(2.0f));
	}


    IEnumerator stopSplash(float time)
    {
        yield return new WaitForSeconds(time);

        GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(true);
        Destroy(this.gameObject);
    }
}
