using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour {

    private void Start()
    {
        GameObject.Find("Canvas").transform.GetChild(5).gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject.Find("Canvas").transform.GetChild(5).gameObject.SetActive(true);
        GetComponent<Collider>().enabled = false;
        StartCoroutine(Fade(0.0f));
    }

    IEnumerator Fade(float time)
    {
        yield return new WaitForSeconds(time);

        if (MapMovement.GameMode == MapMovement.gameMode.PUZZLE) GameObject.Find("Map").GetComponent<MapMovement>().checkPuzzle();
        else GameObject.Find("Map").GetComponent<MapMovement>().checkSurvival();
    }

}
