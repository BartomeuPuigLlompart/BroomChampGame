using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SpeedIA : MonoBehaviour {

    struct rival
    {
        public float time;
        public Vector3 position;
        public Vector3 speed;
        public float distance;
    }

    rival[] Rival;

    GameObject endingObj;
    GameObject player;
    GameObject canvas;

    private float iaDestination;
    private float lowRange, highRange;

	// Use this for initialization
	void Start() {
        Rival = new rival[4];

        player = GameObject.Find("Player");
        canvas = GameObject.Find("Canvas");
        endingObj = GameObject.Find("Ending");

        if (MapMovement.GameMode == MapMovement.gameMode.SPEED)
        {
            lowRange = 15.0f;
            highRange = 35.0f;
            iaDestination = 1000.0f;
        }
        else
        {
            iaDestination = 750.0f;
            lowRange = 20.0f;
            highRange = 30.0f;
        }

        canvas.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Slider>().maxValue = canvas.transform.GetChild(1).GetChild(1).gameObject.GetComponent<Slider>().maxValue = 
        canvas.transform.GetChild(1).GetChild(2).gameObject.GetComponent<Slider>().maxValue =  canvas.transform.GetChild(1).GetChild(3).gameObject.GetComponent<Slider>().maxValue = 
        canvas.transform.GetChild(1).GetChild(4).gameObject.GetComponent<Slider>().maxValue = iaDestination;

        for (int i = 0; i < Rival.Length; i++)
        {
            Rival[i].time = 0.0f;
            Rival[i].position = Vector3.zero;
            for (int j = 0; j < iaDestination / 100; j++) Rival[i].time += Random.Range(lowRange, highRange);
            Rival[i].speed = new Vector3(iaDestination / Rival[i].time, 0.0f, 0.0f);
        }
	}

    // Update is called once per frame
    void Update() {

        List<float> table;
        table = new List<float> { };

        for (int i = 0; i < Rival.Length; i++)
        {
            if (Rival[i].position.x < iaDestination + 1)
            {
                Rival[i].position += Rival[i].speed * Time.deltaTime;
                if(MapMovement.GameMode != MapMovement.gameMode.SPEED && (int)Rival[i].position.x % 150 == 0 && (int)Rival[i].position.x != 0)
                {
                    if (Random.Range(1, 4) == 1) Rival[i].position += new Vector3(-125, 0, 0);
                    else Rival[i].position += new Vector3(1, 0, 0);
                }
            }
            else Rival[i].position = new Vector3(iaDestination, 0, 0);
            Rival[i].distance = Vector3.Distance(Rival[i].position, new Vector3(iaDestination, 0, 0));
            table.Add(Rival[i].distance);
            canvas.transform.GetChild(1).GetChild(i).gameObject.GetComponent<Slider>().value = iaDestination - Rival[i].distance;
        }

        table.Add(Vector3.Distance(player.transform.position, endingObj.transform.position));

        canvas.transform.GetChild(1).GetChild(4).gameObject.GetComponent<Slider>().value = iaDestination - table[4];

        table.Sort();

        for(int i = 0; i < table.Count; i++)
        {
            if (table[i] == Vector3.Distance(player.transform.position, endingObj.transform.position))
            {
                switch(i)
                {
                    case 0:
                        canvas.transform.GetChild(2).GetComponent<Text>().text = "1st";
                        break;
                    case 1:
                        canvas.transform.GetChild(2).GetComponent<Text>().text = "2nd";
                        break;
                    case 2:
                        canvas.transform.GetChild(2).GetComponent<Text>().text = "3rd";
                        break;
                    case 3:
                        canvas.transform.GetChild(2).GetComponent<Text>().text = "4th";
                        break;
                    case 4:
                        canvas.transform.GetChild(2).GetComponent<Text>().text = "5th";
                        break;
                }
            }
        }
    }
}
