using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerCity : MonoBehaviour {
	public GameObject[] enemyType;
	public GameObject player;
	public bool gameOver;

	//Para hacer la oleadas
	int wave=0;
	public int enemyCount;
	int enemyCountInitial;


	//Referencia a los textos;
	public Text wave_txt;
	public Text enemyCount_txt;
	public Text gameOver_txt;
	public Button reiniciar_btn;
	public Button salir_btn;

	//Audios for music
	public AudioClip[] bgMusic;

	// Use this for initialization
	void Start () {
		gameOver = false;
		StartCoroutine ("spawnEnemies");
		enemyCount = 0;
		enemyCountInitial = enemyCount;
		wave_txt.text = "Ola: " + wave.ToString ();
		enemyCount_txt.text = "Enemigos: " + enemyCount.ToString ();
		gameOver_txt.gameObject.SetActive (false);
		reiniciar_btn.gameObject.SetActive (false);
		salir_btn.gameObject.SetActive (false);
		GetComponent<AudioSource> ().clip = bgMusic [Random.Range (0,8)];
		GetComponent<AudioSource> ().Play ();
	}

	void Update(){
		wave_txt.text = "Ola: " + wave.ToString ();
		enemyCount_txt.text = "Enemigos: " + enemyCount.ToString ();
	}

	IEnumerator spawnEnemies(){
		yield return new WaitForSeconds (10f);
		while (gameOver != true) {
			if (enemyCount == 0) {
				wave++;
				enemyCount = Random.Range (3,5) + enemyCountInitial;
				enemyCountInitial = enemyCount;
				for (int i = 0; i < enemyCount; i++) {
					yield return new WaitForSeconds (5f);
					float xRandPos = Random.Range (-2400f, 2400f);
					float zRandPos = Random.Range (-2400f, 2400f);
					float yRandPos = Random.Range (200f, 600f);
					Vector3 spawnPos = new Vector3 (xRandPos, yRandPos, zRandPos);
					Instantiate (enemyType[Random.Range(0,2)], spawnPos, Quaternion.identity);
				}
			}
			yield return new WaitForSeconds (10f);
		}
		gameOver_txt.gameObject.SetActive (true);
		reiniciar_btn.gameObject.SetActive (true);
		salir_btn.gameObject.SetActive (true);
		if (wave > PlayerPrefs.GetInt ("Best Score")) {
			PlayerPrefs.SetInt ("Best Score", wave);
		}
	}


}
