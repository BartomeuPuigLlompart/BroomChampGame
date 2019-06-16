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
        gameObject.AddComponent<SpeedIA>();
        mapFragments = new GameObject[10];
        initialSpeed = new Vector3(-4f, 0, 0);
        mapSpeed = initialSpeed;
        rb = transform.GetComponent<Rigidbody>();
        Instance = this;

        mapFragments[0] = Instantiate(Resources.Load("ResPrefabs/Map Fragments/Fragment_1") as GameObject);
        mapFragments[0].transform.SetParent(transform.GetChild(0).transform);
        mapFragments[0].transform.localPosition = Vector3.zero;
        mapFragments[0].AddComponent<SpeedRender>();

        List<int> fragmNumList = new List<int>();

        for (int n = 2; n < 12; n++)
        {
            fragmNumList.Add(n);
        }

        for (int i = 1; i < 10; i++)
        {
            int idx = Random.Range(0, fragmNumList.Count - 1);
            int fragmentNum = fragmNumList[idx];
            fragmNumList.RemoveAt(idx);
            string directory = "ResPrefabs/Map Fragments/Fragment_" + fragmentNum.ToString();
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
