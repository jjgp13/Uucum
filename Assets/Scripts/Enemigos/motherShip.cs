using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class motherShip : MonoBehaviour {

	public float speed;
	GameObject target;
	public int stamina;
	public GameObject Explosion;

	// Use this for initialization
	void Start () {
		stamina = 50;
		speed = 40f;
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			transform.LookAt (target.transform);
			transform.Translate (Vector3.forward * speed * Time.deltaTime);
		}
		if (stamina <= 0) {
			Instantiate (Explosion, transform.position, transform.rotation);
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "tower") {
			target = other.gameObject;
		}
	}

	void OnCollisionEnter(Collision other){
		Destroy (other.gameObject);
		stamina--;
	}
}
