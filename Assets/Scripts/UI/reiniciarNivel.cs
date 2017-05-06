using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class reiniciarNivel : MonoBehaviour {

	public GameObject panelHistoria;
	public GameObject controller;

	public void restartCurrentScene(){
		Scene scene = SceneManager.GetActiveScene ();
		SceneManager.LoadScene (scene.name);
	}

	public void goToMenu(){
		SceneManager.LoadScene ("main");
	}

	public void iniciarJuego(){
		panelHistoria.SetActive (false);
	}
}