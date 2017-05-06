using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

	public bool followPlayerState;
	public bool inTurretRangeState;
	public bool inTowerRangeState;

	public float speed;

	public GameObject target;
	public GameObject bullet;
	public Transform spawnPos;

	// Use this for initialization
	void Start () {
		followPlayerState = false;
		inTurretRangeState = false;
		inTowerRangeState = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (followPlayerState)
			followPlayer ();
		if (inTurretRangeState)
			destroyTurret ();
		if (inTowerRangeState)
			destroyTower ();
	}

	public void followPlayer(){
		if (target != null) {
			transform.LookAt (target.transform);
			transform.Translate (Vector3.forward * speed * Time.deltaTime);
		}
	}

	public void destroyTurret(){
		transform.LookAt (target.transform);
		transform.RotateAround (target.transform.position, Vector3.up, 10 * Time.deltaTime);
	}

	public void destroyTower(){
		transform.LookAt (target.transform);
		speed = 75f;
		transform.Translate (Vector3.forward * speed * Time.deltaTime);
	}

	public IEnumerator shoot(){
		while(true){
			Instantiate(bullet, spawnPos.position, spawnPos.rotation);
			yield return new WaitForSeconds (2);
		}
	}
}