using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapMovement : MonoBehaviour {

    public Vector3 mapSpeed;
    public Vector3 initialSpeed;
    Rigidbody rb;
    public static MapMovement Instance;
    private GameObject[] mapFragments;
    private GameObject canvas;

    public enum gameMode { SPEED, PUZZLE, SURVIVAL };

    public static gameMode GameMode;

    // Use this for initialization
    void Start() {
        GameMode = (gameMode)(SceneManager.GetActiveScene().buildIndex - 1);
        canvas = GameObject.Find("Canvas");
        gameObject.AddComponent<SpeedIA>();
        if (GameMode == gameMode.SPEED) mapFragments = new GameObject[10];
        else mapFragments = new GameObject[5];
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

        switch (GameMode)
        {
            case gameMode.SPEED:
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
                break;
            case gameMode.SURVIVAL:
            case gameMode.PUZZLE:
                float lastXPos = 100.0f;
                GameObject conectionFrag;
                conectionFrag = Instantiate(Resources.Load("ResPrefabs/Map Fragments/Conection Fragment") as GameObject);
                conectionFrag.transform.SetParent(transform.GetChild(0).transform);
                conectionFrag.transform.localPosition = new Vector3(lastXPos, 0.0f, 0.0f);
                for (int i = 1; i < 5; i++)
                {
                    lastXPos += 50.0f;

                    int idx = Random.Range(0, fragmNumList.Count - 1);
                    int fragmentNum = fragmNumList[idx];
                    fragmNumList.RemoveAt(idx);
                    string directory = "ResPrefabs/Map Fragments/Fragment_" + fragmentNum.ToString();
                    mapFragments[i] = Instantiate(Resources.Load(directory) as GameObject);
                    mapFragments[i].transform.SetParent(transform.GetChild(0).transform);
                    mapFragments[i].transform.localPosition = new Vector3(lastXPos, -150.0f, 0.0f);
                    lastXPos += 100.0f;
                }
                conectionFrag.transform.SetAsLastSibling();
                break;
        }


    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && !canvas.transform.GetChild(3).gameObject.activeSelf)
        {
            Time.timeScale = 0.0f;
            canvas.transform.GetChild(3).gameObject.SetActive(true);
        }
        rb.velocity = (mapSpeed);
    }

    public void checkPuzzle()
    {
        GameObject cloneFrag;
        if (transform.GetChild(0).GetChild(transform.GetChild(0).childCount - 2).gameObject.name.Substring(0, 9) == "Conection")
            Destroy(transform.GetChild(0).GetChild(transform.GetChild(0).childCount - 2).gameObject);
        if (transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).childCount > 0)
            {
        cloneFrag = Instantiate(Resources.Load("ResPrefabs/Map Fragments/" + transform.GetChild(0).GetChild(2).gameObject.name.Substring(0, 10)) as GameObject);
        Destroy(transform.GetChild(0).GetChild(2).gameObject);
        cloneFrag.transform.SetParent(transform.GetChild(0).transform);
        cloneFrag.transform.SetSiblingIndex(2);
        transform.GetChild(0).GetChild(1).transform.localPosition += new Vector3(150, 0, 0);
        }
        else
        {
            cloneFrag = transform.GetChild(0).GetChild(3).gameObject;
            Destroy(transform.GetChild(0).GetChild(2).gameObject);
            cloneFrag.transform.localPosition += new Vector3(0, 150, 0);
        }
        
        cloneFrag.transform.localPosition = transform.GetChild(0).GetChild(transform.GetChild(0).childCount - 1).transform.localPosition + new Vector3(50, 0, 0);

        GameObject conectionFrag;
        conectionFrag = Instantiate(Resources.Load("ResPrefabs/Map Fragments/Conection Fragment") as GameObject);
        conectionFrag.transform.SetParent(transform.GetChild(0).transform);
        conectionFrag.transform.localPosition = cloneFrag.transform.localPosition + new Vector3(100, 0, 0);
        conectionFrag.transform.SetAsLastSibling();

        GameObject.Find("Canvas").transform.GetChild(5).gameObject.SetActive(false);
    }

    public void checkSurvival()
    {
        GameObject cloneFrag;
        if (transform.GetChild(0).GetChild(transform.GetChild(0).childCount - 2).gameObject.name.Substring(0, 9) == "Conection")
            Destroy(transform.GetChild(0).GetChild(transform.GetChild(0).childCount - 2).gameObject);
        if (transform.GetChild(0).GetChild(2).GetChild(0).GetChild(2).childCount > 0)
        {
            cloneFrag = Instantiate(Resources.Load("ResPrefabs/Map Fragments/" + transform.GetChild(0).GetChild(2).gameObject.name.Substring(0, 10)) as GameObject);
            Destroy(transform.GetChild(0).GetChild(2).gameObject);
            cloneFrag.transform.SetParent(transform.GetChild(0).transform);
            cloneFrag.transform.SetSiblingIndex(2);
            transform.GetChild(0).GetChild(1).transform.localPosition += new Vector3(150, 0, 0);
        }
        else
        {
            cloneFrag = transform.GetChild(0).GetChild(3).gameObject;
            Destroy(transform.GetChild(0).GetChild(2).gameObject);
            cloneFrag.transform.localPosition += new Vector3(0, 150, 0);
        }

        cloneFrag.transform.localPosition = transform.GetChild(0).GetChild(transform.GetChild(0).childCount - 1).transform.localPosition + new Vector3(50, 0, 0);

        GameObject conectionFrag;
        conectionFrag = Instantiate(Resources.Load("ResPrefabs/Map Fragments/Conection Fragment") as GameObject);
        conectionFrag.transform.SetParent(transform.GetChild(0).transform);
        conectionFrag.transform.localPosition = cloneFrag.transform.localPosition + new Vector3(100, 0, 0);
        conectionFrag.transform.SetAsLastSibling();

        GameObject.Find("Canvas").transform.GetChild(5).gameObject.SetActive(false);
    }
}
