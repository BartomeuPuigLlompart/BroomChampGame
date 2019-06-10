using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turbo : MonoBehaviour {

    ParticleSystem effect;
    GameObject player;

	// Use this for initialization
	void Start () {
        effect = transform.GetChild(0).GetComponent<ParticleSystem>();
        effect.enableEmission = false;
        player = GameObject.Find("Player");
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            player.GetComponent<Movement>().activateTurbo();
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
