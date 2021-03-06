﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour {

	private GameObject mainPanel;
	public GameObject howToPanel;

	void Start(){
		mainPanel = GameObject.Find ("BullePanel");
		mainPanel.SetActive (false);
		StartCoroutine (launchStart ());
	}

	public IEnumerator launchStart(){
		yield return new WaitForSeconds (1.3f);
		mainPanel.SetActive (true);
	}

	public void quitGame(){
		Application.Quit ();
	}

	public void launchGame(){
		SceneManager.LoadScene ("Game");
	}

	public void launchHowToPanel(){
		howToPanel.SetActive (true);

	}
	public void quitHowToPanel(){
		howToPanel.SetActive (false);

	}

}
