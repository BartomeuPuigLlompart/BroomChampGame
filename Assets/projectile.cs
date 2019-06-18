using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour {

    // Use this for initialization

    Vector3 direction;
    Vector3 startPos;
    GameObject target;
    private GameObject explosion;
    private AudioSource source;
    bool proximity;
    float distAux = 30.0f;
    void Start () {
        proximity = false;
        transform.SetParent(GameObject.Find("Player").transform.GetChild(2));
        transform.localPosition = Vector3.zero;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        source = GameObject.Find("Player").transform.GetChild(2).GetComponent<AudioSource>();
        source.Play();
        if (enemies.Length > 0)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if (Vector3.Distance(transform.position, enemies[i].transform.position) < distAux)
                {
                    distAux = Vector3.Distance(transform.position, enemies[i].transform.position);
                    target = enemies[i];
                    transform.LookAt(target.transform.position);
                }
            }
        }
        StartCoroutine(stopParticle(4.0f));
        transform.SetParent(null);
        explosion = transform.GetChild(1).gameObject;
        explosion.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (target == null) Destroy(gameObject);
        else
        {
            transform.position = Vector3.Lerp(target.transform.position, transform.position, 20 * Time.deltaTime);
            if (Vector3.Distance(transform.position, target.transform.position) < 1f) killEnemy();
        }
    }

    IEnumerator stopParticle(float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(explosion);
        Destroy(this.gameObject);
    }

    void killEnemy()
    {
        explosion.SetActive(true);
        explosion.transform.SetParent(GameObject.Find("Map").transform);
        transform.GetComponent<Collider>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        Destroy(target.gameObject);
    }
}
