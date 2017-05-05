using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroByBullet : MonoBehaviour {

	public GameObject explosion;
	public GameObject controller;

	void Start(){
		controller = GameObject.Find ("GameController");
	}

	void OnCollisionEnter(Collision other){
		controller.GetComponent<GameControllerCity> ().enemyCount--;
		Instantiate (explosion, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}
}
