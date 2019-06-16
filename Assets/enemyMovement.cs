using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour {
    private bool move;
    private GameObject Player;
	// Use this for initialization
	void Start () {
        move = false;
        Player = GameObject.Find("Player");
        transform.tag = "enemy";
    }
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(transform.position, Player.transform.position) < 20.0f)
        {
            move = true;
        }
        if (move)
        {
            transform.position += new Vector3(-0.03f, 0, 0);
            if (Vector3.Distance(transform.position, Player.transform.position) > 25.0f) Destroy(gameObject);
        }
    }
}
