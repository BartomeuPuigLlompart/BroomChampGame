using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomPowerUp : MonoBehaviour {

    

    enum powerUpType { SPEED, DEFFENSE, ATACK};
    
    
    // Use this for initialization
    void Start()
    {
        powerUP PowerUp;
        int powerUpNum = gameObject.transform.childCount; ;
        powerUpType[] pType = new powerUpType[powerUpNum];
        GameObject[] aux = new GameObject[powerUpNum];
        for (int i = 0; i < powerUpNum; i++)
        {
            pType[i] = (powerUpType)Random.Range(0.0f, 3.0f);
            switch (pType[i])
            {
                case powerUpType.ATACK:
                    Instantiate(Resources.Load("ResPrefabs/atackPowerUp"));
                    break;
                case powerUpType.DEFFENSE:
                    Instantiate(Resources.Load("ResPrefabs/deffensePowerUp"));
                    break;
                case powerUpType.SPEED:
                    Instantiate(Resources.Load("ResPrefabs/speedPowerUp"));
                    break;
            }
        }
        aux = GameObject.FindGameObjectsWithTag("PowerUp");
        for (int i = 0; i < powerUpNum; i++)
        {
            aux[i].transform.SetParent(this.gameObject.transform.GetChild(i).transform);
            aux[i].transform.localPosition = Vector3.zero;

            switch (pType[i])
            {
                case powerUpType.ATACK:
                    aux[i].AddComponent<AtackPowerUp>();
                    break;
                case powerUpType.DEFFENSE:
                    aux[i].AddComponent<DefensePowerUp>();
                    break;
                case powerUpType.SPEED:
                    aux[i].AddComponent<SpeedPowerUp>();
                    break;
            }
        }
    }
}
