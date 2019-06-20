using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class checkPoint : MonoBehaviour {

    private Image fadeImg;

    private Color originalColor;

    public static checkPoint Instance;

    private void Start()
    {
        Instance = this;
       fadeImg = GameObject.Find("Canvas").transform.GetChild(6).GetComponent<Image>();
        originalColor = fadeImg.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject.Find("Canvas").transform.GetChild(6).gameObject.SetActive(true);
        GetComponent<Collider>().enabled = false;
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {

        Color c = fadeImg.color;
        c.a = 0.0f;


        //Mientras no esté totalmente visible va aumentando su visibilidad
        while (c.a < 1)
        {
            fadeImg.color = c;
            c.a += 0.04f;
            yield return null;
        }


        c.a = 0.0f;


        if (MapMovement.GameMode == MapMovement.gameMode.PUZZLE) GameObject.Find("Map").GetComponent<MapMovement>().checkPuzzle();
        else GameObject.Find("Map").GetComponent<MapMovement>().checkSurvival();
        
    }

    public IEnumerator FadeOff(bool checkAns)
    {
        Color c;
        c = fadeImg.color;
        c.a = 1.0f;

        if (!checkAns)
        {
            c.r = 255;
            c.g = 0;
            c.b = 0;
        }

        else
        {
            c.r = 0;
            c.g = 255;
            c.b = 0;
        }
        while (c.a > 0)
        {
            fadeImg.color = c;
            c.a -= 0.04f;
            yield return null;
        }

        fadeImg.color = originalColor;

        fadeImg.gameObject.SetActive(false);
    }

}
