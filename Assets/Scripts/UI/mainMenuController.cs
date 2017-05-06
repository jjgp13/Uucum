using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenuController : MonoBehaviour {

	public GameObject mainPanel;
	public GameObject insPanel;
	public GameObject colaPanel;
	public Text bestScore;

	void Start(){
		bestScore.text = PlayerPrefs.GetInt ("Best Score").ToString ();
	}

	public void startToPlay(){
		SceneManager.LoadScene ("CityScene");
	}

	public void showIns(){
		mainPanel.SetActive (false);
		colaPanel.SetActive (false);
		insPanel.SetActive (true);
	}

	public void showCola(){
		mainPanel.SetActive (false);
		colaPanel.SetActive (true);
		insPanel.SetActive (false);
	}

	public void showMainPanel(){
		mainPanel.SetActive (true);
		colaPanel.SetActive (false);
		insPanel.SetActive (false);
	}
}
