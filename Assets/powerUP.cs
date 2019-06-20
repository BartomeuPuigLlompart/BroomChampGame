using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class powerUP : MonoBehaviour {

    public enum comboKey{UP, DOWN, LEFT, RIGHT}
    public comboKey ComboKey;
    public float duration;
    public float activationTime;
    ParticleSystem effect;
    GameObject player;
    public Sprite image;
    AudioSource source;
    // Use this for initialization
    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        source = transform.GetComponent<AudioSource>();
        source.clip = Resources.Load<AudioClip>("Music/effects/power_up");
        activationTime = 0.0f;
        if (transform.childCount != 0)
        {
            effect = transform.GetChild(0).GetComponent<ParticleSystem>();
            effect.enableEmission = false;
        }
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            source.Play();
            effect.enableEmission = true;
            power_UP_Effect();
            this.GetComponent<MeshRenderer>().enabled = false;
            this.GetComponent<Collider>().enabled = false;
            gameObject.transform.SetParent(GameObject.Find("Map").transform.GetChild(1));
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
        if (full) StartCoroutine(GameObject.Find("Player").GetComponent<Movement>().killPlayer(3.0f, 3));
    }

}

public class DefensePowerUp : powerUP
{

    public override void power_UP_Effect()
    {
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
        if (full) StartCoroutine(GameObject.Find("Player").GetComponent<Movement>().killPlayer(3.0f, 3));
    }

}

public class AtackPowerUp : powerUP
{

    public override void power_UP_Effect()
    {
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
        if (full) StartCoroutine(GameObject.Find("Player").GetComponent<Movement>().killPlayer(3.0f, 3));
    }

}
