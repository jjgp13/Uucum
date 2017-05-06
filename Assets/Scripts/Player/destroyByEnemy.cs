using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyByEnemy : MonoBehaviour {

	public GameObject controller;
	public GameObject explosion;

	void OnCollisionEnter(Collision other){
		Instantiate (explosion, transform.position, transform.rotation);
		controller.GetComponent<GameControllerCity> ().gameOver = true;
		Destroy (gameObject);
	}
}
