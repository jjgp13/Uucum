using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caraNave : MonoBehaviour {

	public GameObject target;
	float speed;

	// Use this for initialization
	void Start () {
		speed = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			transform.LookAt (target.transform);
			transform.Translate(Vector3.forward * speed);
		}else{
			transform.LookAt (GameObject.FindGameObjectWithTag("tower").transform);
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "player") {
			target = other.gameObject;
		}
	}
}
