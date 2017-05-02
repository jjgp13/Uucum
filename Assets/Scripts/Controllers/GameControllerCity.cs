using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerCity : MonoBehaviour {

	public int timeToSpawnEnemies = 7;
	public GameObject enemySpaceShip;

	// Use this for initialization
	void Start () {
		StartCoroutine ("spawnEnemies");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator spawnEnemies(){
		while (true) {
			yield return new WaitForSeconds (timeToSpawnEnemies);
			float xRandPos = Random.Range (-2400f, 2400f);
			float zRandPos = Random.Range (-2400f, 2400f);
			float yRandPos = Random.Range (300f, 600f);
			Vector3 spawnPos = new Vector3 (xRandPos, yRandPos, zRandPos);
			Instantiate (enemySpaceShip, spawnPos, Quaternion.identity);
		}
	}
}
