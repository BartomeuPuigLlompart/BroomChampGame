using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class turbo : MonoBehaviour {

    ParticleSystem effect;
    GameObject player;
    public static float turboRef;
    public static float turboMax = 2.0f;

    private AudioSource source;

	// Use this for initialization
	void Start () {
        if (!PlayerPrefs.HasKey("Tuto2")) PlayerPrefs.SetInt("Tuto2", 0);
        if (!PlayerPrefs.HasKey("Tuto3")) PlayerPrefs.SetInt("Tuto3", 0);
        if (!PlayerPrefs.HasKey("Tuto4")) PlayerPrefs.SetInt("Tuto4", 0);
        gameObject.AddComponent<AudioSource>();
        source = GetComponent<AudioSource>();
        source.clip = Resources.Load<AudioClip>("Music/effects/item");
        effect = transform.GetChild(0).GetComponent<ParticleSystem>();
        effect.enableEmission = false;
        player = GameObject.Find("Player");
        if (MapMovement.GameMode == MapMovement.gameMode.PUZZLE) GetComponent<Renderer>().sharedMaterial = Resources.Load<Material>("Sprites/GreenCircle");
        else if (MapMovement.GameMode == MapMovement.gameMode.SURVIVAL) GetComponent<Renderer>().sharedMaterial = Resources.Load<Material>("Sprites/RedCircle");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            if (PlayerPrefs.GetInt("Tuto2") == 0 && MapMovement.GameMode == MapMovement.gameMode.SPEED)
            {
                tutorialManager.Instance.activateTutorial(2);
                PlayerPrefs.SetInt("Tuto2", 1);
            }
            else if (PlayerPrefs.GetInt("Tuto3") == 0 && MapMovement.GameMode == MapMovement.gameMode.PUZZLE)
            {
                tutorialManager.Instance.activateTutorial(3);
                PlayerPrefs.SetInt("Tuto3", 1);
            }
            else if (PlayerPrefs.GetInt("Tuto4") == 0 && MapMovement.GameMode == MapMovement.gameMode.SURVIVAL)
            {
                tutorialManager.Instance.activateTutorial(4);
                PlayerPrefs.SetInt("Tuto4", 1);
            }
            source.Play();
            if (MapMovement.GameMode == MapMovement.gameMode.SPEED) turboRef = Time.realtimeSinceStartup;
            else if(MapMovement.GameMode == MapMovement.gameMode.SURVIVAL) player.GetComponent<Movement>().extraAtack();
            effect.enableEmission = true;
            this.GetComponent<MeshRenderer>().enabled = false;
            StartCoroutine(stopParticle(2));
        }
    }

    IEnumerator stopParticle(float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(this.gameObject);
    }
}
