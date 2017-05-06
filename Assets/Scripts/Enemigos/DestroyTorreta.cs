using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTorreta : MonoBehaviour {

	public GameObject explosion;

	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag == "Bullet") {
			Instantiate(explosion, transform.position, Quaternion.identity);
			StopCoroutine (GetComponent<torreta>().shoot ());
			Destroy(gameObject);
		}
	}
}