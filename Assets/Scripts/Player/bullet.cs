using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

    public float speed;

    // Use this for initialization
    void Start () {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
		Destroy (gameObject, 1f);
    }

	void OnCollisionEnter(Collision other){
		Destroy (gameObject);
	}
}
