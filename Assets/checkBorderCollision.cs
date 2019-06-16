using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkBorderCollision : MonoBehaviour {

    GameObject player;
    static double colInmu;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        colInmu = Time.realtimeSinceStartup - 1.5f;
	}

   void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && colInmu + 1.5f < Time.realtimeSinceStartup)
        {
            player.GetComponent<Movement>().lives--;
            if (player.GetComponent<Movement>().lives == 1) player.GetComponent<Movement>().lowHealthRef = Time.realtimeSinceStartup;
            else if (player.GetComponent<Movement>().lives == 0) player.GetComponent<Movement>().killPlayer();
            colInmu = Time.realtimeSinceStartup;
        }

    }
}
