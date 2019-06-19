using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSruvivalRenderer : MonoBehaviour {

    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        for (int i = 2; i < transform.childCount; i++) transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update () {
		if(transform.localPosition.y > -100 && transform.GetChild(2).GetComponent<MeshRenderer>().enabled == false)
        {
            for (int i = 2; i < transform.childCount; i++) transform.GetChild(i).GetComponent<MeshRenderer>().enabled = true;
        }
	}
}
