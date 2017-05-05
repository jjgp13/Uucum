using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class reiniciarNivel : MonoBehaviour {

	public void restartCurrentScene(){
		Scene scene = SceneManager.GetActiveScene ();
		SceneManager.LoadScene (scene.name);
	}

	public void goToMenu(){
		SceneManager.LoadScene ("main");
	}
}