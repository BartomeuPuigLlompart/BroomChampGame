using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMovement : MonoBehaviour {

    public Vector3 mapSpeed;
    public Vector3 initialSpeed;
    Rigidbody rb;
    public static MapMovement Instance;

    // Use this for initialization
    void Start () {
        initialSpeed = new Vector3(-5f, 0, 0);
        mapSpeed = initialSpeed;
        rb = transform.GetComponent<Rigidbody>();
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
         rb.velocity = (mapSpeed);
	}
}
