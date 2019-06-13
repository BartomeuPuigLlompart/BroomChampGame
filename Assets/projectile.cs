using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour {

    // Use this for initialization

    Vector3 direction;
    Vector3 startPos;
    Rigidbody rb;
    private GameObject explosion;
	void Start () {
        Vector3 LocalPos = transform.position;
        transform.SetParent(GameObject.Find("Player").transform.GetChild(2));
        transform.localPosition = LocalPos;
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition - new Vector3(0, 0, GameObject.Find("Main Camera").transform.position.z));
        direction = new Vector3(direction.x - 0.25f, direction.y, 0.0f); 
        transform.LookAt(direction);
        StartCoroutine(stopParticle(4.0f));
        transform.SetParent(null);
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        rb.velocity = transform.forward * 5;
        explosion = transform.GetChild(1).gameObject;
        explosion.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        rb.AddForce(transform.forward * 10);
        rb.AddForce(MapMovement.Instance.mapSpeed);
	}

    IEnumerator stopParticle(float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(explosion);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Player")
        {
            explosion.SetActive(true);
            explosion.transform.SetParent(GameObject.Find("Map").transform);
            transform.GetComponent<Collider>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
