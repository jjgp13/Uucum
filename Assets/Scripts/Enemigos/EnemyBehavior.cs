using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

	public bool followPathToTowerState;
	public bool followPlayerState;

	public float speed = 25f;

	public GameObject target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (followPlayerState)
			followPlayer (); 
	}

	public void followPlayer(){
		transform.LookAt (target.transform);
		transform.Translate (Vector3.forward * speed * Time.deltaTime);
	}


}
