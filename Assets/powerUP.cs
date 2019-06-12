using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class powerUP : MonoBehaviour {

    public enum comboKey{UP, DOWN, LEFT, RIGHT}
    public comboKey ComboKey;
    public float duration;
    ParticleSystem effect;
    GameObject player;
    public Sprite image; 
    // Use this for initialization
    void Start()
    {
        //ComboKey = (comboKey)Random.Range(0.0f, 4.0f);
        duration = 4.0f;
        effect = transform.GetChild(0).GetComponent<ParticleSystem>();
        effect.enableEmission = false;
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            effect.enableEmission = true;
            power_UP_Effect();
            this.GetComponent<MeshRenderer>().enabled = false;
            this.GetComponent<Collider>().enabled = false;
            StartCoroutine(stopParticle(2));
        }
    }

    public virtual void power_UP_Effect() { }

    IEnumerator stopParticle(float time)
    {
        yield return new WaitForSeconds(time);

        effect.enableEmission = false;
    }
}

public class SpeedPowerUp : powerUP
{

    public override void power_UP_Effect() {
        Debug.Log("speed");
        bool full = true;
        for (int i = 0; i < 4; i++)
        {
            if (Movement.PowerUpBag.StoredSpeedPowerUp[i] == null)
            {
                Movement.PowerUpBag.StoredSpeedPowerUp[i] = this;
                Color c;
                c = Movement.PowerUpBag.StoredSpeedPowerUpSprite[i].GetComponent<Image>().color;
                c.a = 255;
                Movement.PowerUpBag.StoredSpeedPowerUpSprite[i].GetComponent<Image>().color = c;
                Movement.PowerUpBag.StoredSpeedPowerUpSprite[i].GetComponent<Image>().sprite = image;
                full = false;
                return;
            }
        }
        if (full) GameObject.Find("Player").GetComponent<Movement>().killPlayer();
    }

}

public class DefensePowerUp : powerUP
{

    public override void power_UP_Effect()
    {
        Debug.Log("defense");
        bool full = true;
        for (int i = 0; i < 4; i++)
        {
            if (Movement.PowerUpBag.StoredDeffensePowerUp[i] == null)
            {
                Movement.PowerUpBag.StoredDeffensePowerUp[i] = this;
                Color c;
                c = Movement.PowerUpBag.StoredDeffensePowerUpSprite[i].GetComponent<Image>().color;
                c.a = 255;
                Movement.PowerUpBag.StoredDeffensePowerUpSprite[i].GetComponent<Image>().color = c;
                Movement.PowerUpBag.StoredDeffensePowerUpSprite[i].GetComponent<Image>().sprite = image;
                full = false;
                break;
            }
        }
        if (full) GameObject.Find("Player").GetComponent<Movement>().killPlayer();
    }

}

public class AtackPowerUp : powerUP
{

    public override void power_UP_Effect()
    {
        Debug.Log("atack");
        bool full = true;
        for (int i = 0; i < 4; i++)
        {
            if (Movement.PowerUpBag.StoredAtackPowerUp[i] == null)
            {
                Movement.PowerUpBag.StoredAtackPowerUp[i] = this;
                Color c;               
                c = Movement.PowerUpBag.StoredAtackPowerUpSprite[i].GetComponent<Image>().color;
                c.a = 255;
                Movement.PowerUpBag.StoredAtackPowerUpSprite[i].GetComponent<Image>().color = c;
                Movement.PowerUpBag.StoredAtackPowerUpSprite[i].GetComponent<Image>().sprite = image;
                full = false;
                break;
            }
        }
        if (full) GameObject.Find("Player").GetComponent<Movement>().killPlayer();
    }

}
