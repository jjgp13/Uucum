using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnRuins : MonoBehaviour {

	public GameObject[] ruins;

	// Use this for initialization
	void Start () {
		StartCoroutine (spawnRuinsCo());
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	IEnumerator spawnRuinsCo(){
		yield return new WaitForSeconds(5f);
		for (int i = 0; i < 100; i++) {
			float xRandPos = Random.Range (-200f, 450f);
			float zRandPos = Random.Range (-0f, 300f);
			float yRandPos = Random.Range (0f, 20f);
			Vector3 spawnPos = new Vector3 (xRandPos, yRandPos, zRandPos);
			Instantiate (ruins[Random.Range(0,18)], spawnPos, Quaternion.identity);
		}
	}

}
