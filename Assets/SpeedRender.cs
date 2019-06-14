using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedRender : MonoBehaviour {

    // Use this for initialization

    private GameObject player;

	void Start () {
        player = GameObject.Find("Player");
        for (int i = 2; i < transform.childCount; i++) transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(player.transform.position, transform.position) < 50.0f && transform.GetChild(2).GetComponent<MeshRenderer>().enabled == false)
        {
            for (int i = 2; i < transform.childCount; i++) transform.GetChild(i).GetComponent<MeshRenderer>().enabled = true;
        }

        else if (transform.GetChild(2).GetComponent<MeshRenderer>().enabled == true && Vector3.Distance(player.transform.position, transform.position) > 200.0f) Destroy(gameObject);
	}
}
