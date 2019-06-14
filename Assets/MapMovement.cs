using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMovement : MonoBehaviour {

    public Vector3 mapSpeed;
    public Vector3 initialSpeed;
    Rigidbody rb;
    public static MapMovement Instance;
    private GameObject[] mapFragments;

    // Use this for initialization
    void Start () {
        mapFragments = new GameObject[10];
        initialSpeed = new Vector3(-5f, 0, 0);
        mapSpeed = initialSpeed;
        rb = transform.GetComponent<Rigidbody>();
        Instance = this;

        mapFragments[0] = Instantiate(Resources.Load("ResPrefabs/Map Fragments/Fragment_1") as GameObject);
        mapFragments[0].transform.SetParent(transform.GetChild(0).transform);
        mapFragments[0].transform.localPosition = Vector3.zero;
        mapFragments[0].AddComponent<SpeedRender>();

        for (int i = 1; i < 10; i++)
        {
            string directory = "ResPrefabs/Map Fragments/Fragment_" + ((int)Random.Range(1.0f, 6.0f)).ToString();
            mapFragments[i] = Instantiate(Resources.Load(directory) as GameObject);
            mapFragments[i].transform.SetParent(transform.GetChild(0).transform);
            mapFragments[i].transform.localPosition = new Vector3(i * 100.0f, 0.0f, 0.0f);
            mapFragments[i].AddComponent<SpeedRender>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        rb.velocity = (mapSpeed);
    }
}
