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

	// Use this for initialization
	void Start() {
        Rival = new rival[4];

        player = GameObject.Find("Player");
        canvas = GameObject.Find("Canvas");
        endingObj = GameObject.Find("Ending");

        for (int i = 0; i < Rival.Length; i++)
        {
            Rival[i].time = 0.0f;
            Rival[i].position = Vector3.zero;
            for (int j = 0; j < 10; j++) Rival[i].time += Random.RandomRange(15.0f, 25.0f);
            Rival[i].speed = new Vector3(1000.0f / Rival[i].time, 0.0f, 0.0f);
        }
	}

    // Update is called once per frame
    void Update() {

        List<float> table;
        table = new List<float> { };

        for (int i = 0; i < Rival.Length; i++)
        {
            Rival[i].position += Rival[i].speed * Time.deltaTime;
            Rival[i].distance = Vector3.Distance(Rival[i].position, new Vector3(1000.0f, 0, 0));
            table.Add(Rival[i].distance);
            canvas.transform.GetChild(1).GetChild(i).gameObject.GetComponent<Slider>().value = 1000.0f - Rival[i].distance;
        }

        table.Add(Vector3.Distance(player.transform.position, endingObj.transform.position));

        canvas.transform.GetChild(1).GetChild(4).gameObject.GetComponent<Slider>().value = 1000 - table[4];

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
