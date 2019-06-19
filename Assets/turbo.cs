using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turbo : MonoBehaviour {

    ParticleSystem effect;
    GameObject player;
    public static float turboRef;
    public static float turboMax = 2.0f;

    private AudioSource source;

	// Use this for initialization
	void Start () {
        gameObject.AddComponent<AudioSource>();
        source = GetComponent<AudioSource>();
        source.clip = Resources.Load<AudioClip>("Music/effects/item");
        effect = transform.GetChild(0).GetComponent<ParticleSystem>();
        effect.enableEmission = false;
        player = GameObject.Find("Player");
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
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
