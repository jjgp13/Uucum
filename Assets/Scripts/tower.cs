using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower : MonoBehaviour {

	public GameObject controller;
	public GameObject explosion;
	public int resistence;


	// Use this for initialization
	void Start () {
		resistence = 20;
	}
	
	// Update is called once per frame
	void Update () {
		if (resistence <= 0) {
			controller.GetComponent<GameControllerCity> ().gameOver = true;
			Instantiate (explosion, transform.position, transform.rotation);
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Enemy") {
			Destroy (other.gameObject);
			resistence-=3;
		}
		if (other.gameObject.tag == "bigEnemy") {
			Destroy (other.gameObject);
			resistence-=10;
		}
	}


}
