using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomPowerUp : MonoBehaviour {

    

    enum powerUpType { SPEED, DEFFENSE, ATACK};
    
    
    // Use this for initialization
    void Start()
    {
        int powerUpNum = gameObject.transform.childCount;
        powerUpType[] pType = new powerUpType[powerUpNum];
        GameObject[] aux = new GameObject[powerUpNum];
        Debug.Log(powerUpNum);
        for (int i = 0; i < powerUpNum; i++)
        {
            pType[i] = (powerUpType)Random.Range(0.0f, 3.0f);
            switch (pType[i])
            {
                case powerUpType.ATACK:
                    aux[i] = Instantiate(Resources.Load("ResPrefabs/atackPowerUp") as GameObject);
                    break;
                case powerUpType.DEFFENSE:
                    aux[i] = Instantiate(Resources.Load("ResPrefabs/deffensePowerUp") as GameObject);
                    break;
                case powerUpType.SPEED:
                    aux[i] = Instantiate(Resources.Load("ResPrefabs/speedPowerUp") as GameObject);
                    break;
            }
        }
        for (int i = 0; i < powerUpNum; i++)
        {
            aux[i].transform.SetParent(this.gameObject.transform.GetChild(i).transform);
            aux[i].transform.localPosition = Vector3.zero;

            switch (pType[i])
            {
                case powerUpType.ATACK:
                    aux[i].AddComponent<AtackPowerUp>();
                    aux[i].GetComponent<AtackPowerUp>().ComboKey = (powerUP.comboKey)Random.Range(0.0f, 4.0f);
                    switch (aux[i].GetComponent<AtackPowerUp>().ComboKey)
                    {
                        case powerUP.comboKey.UP:
                            aux[i].GetComponent<AtackPowerUp>().image = Resources.Load<Sprite>("Sprites/RedSquare_UP");
                            break;
                        case powerUP.comboKey.DOWN:
                            aux[i].GetComponent<AtackPowerUp>().image = Resources.Load<Sprite>("Sprites/RedSquare_DOWN");
                            break;
                        case powerUP.comboKey.LEFT:
                            aux[i].GetComponent<AtackPowerUp>().image = Resources.Load<Sprite>("Sprites/RedSquare_LEFT");
                            break;
                        case powerUP.comboKey.RIGHT:
                            aux[i].GetComponent<AtackPowerUp>().image = Resources.Load<Sprite>("Sprites/RedSquare_RIGHT");
                            break;
                    }
                    break;
                case powerUpType.DEFFENSE:
                    aux[i].AddComponent<DefensePowerUp>();
                    aux[i].GetComponent<DefensePowerUp>().ComboKey = (powerUP.comboKey)Random.Range(0.0f, 4.0f);
                    switch (aux[i].GetComponent<DefensePowerUp>().ComboKey)
                    {
                        case powerUP.comboKey.UP:
                            aux[i].GetComponent<DefensePowerUp>().image = Resources.Load<Sprite>("Sprites/GreenSquare_UP");
                            break;
                        case powerUP.comboKey.DOWN:
                            aux[i].GetComponent<DefensePowerUp>().image = Resources.Load<Sprite>("Sprites/GreenSquare_DOWN");
                            break;
                        case powerUP.comboKey.LEFT:
                            aux[i].GetComponent<DefensePowerUp>().image = Resources.Load<Sprite>("Sprites/GreenSquare_LEFT");
                            break;
                        case powerUP.comboKey.RIGHT:
                            aux[i].GetComponent<DefensePowerUp>().image = Resources.Load<Sprite>("Sprites/GreenSquare_RIGHT");
                            break;
                    }
                    break;
                case powerUpType.SPEED:
                    aux[i].AddComponent<SpeedPowerUp>();
                    aux[i].GetComponent<SpeedPowerUp>().ComboKey = (powerUP.comboKey)Random.Range(0.0f, 4.0f);
                    switch (aux[i].GetComponent<SpeedPowerUp>().ComboKey)
                    {
                        case powerUP.comboKey.UP:
                            aux[i].GetComponent<SpeedPowerUp>().image = Resources.Load<Sprite>("Sprites/BlueSquare_UP");
                            break;
                        case powerUP.comboKey.DOWN:
                            aux[i].GetComponent<SpeedPowerUp>().image = Resources.Load<Sprite>("Sprites/BlueSquare_DOWN");
                            break;
                        case powerUP.comboKey.LEFT:
                            aux[i].GetComponent<SpeedPowerUp>().image = Resources.Load<Sprite>("Sprites/BlueSquare_LEFT");
                            break;
                        case powerUP.comboKey.RIGHT:
                            aux[i].GetComponent<SpeedPowerUp>().image = Resources.Load<Sprite>("Sprites/BlueSquare_RIGHT");
                            break;
                    }
                    break;
            }
        }
    }
}
