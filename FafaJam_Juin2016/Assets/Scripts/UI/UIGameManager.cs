using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameManager : MonoBehaviour {

	public GameObject pausePanel;
	public GameObject gameOverPanel;

	public Text finalScoreText;

	public GameObject bossPanel;

	void Start () {
		pausePanel.SetActive (false);
		gameOverPanel.SetActive (false);
		bossPanel.SetActive (false);
	}

	public void displayFinalScore(float value){
		finalScoreText.text = value.ToString ();
	}

	public void launchBossPanel(){
		bossPanel.SetActive (true);
	}
	public void stopBossPanel(){
		bossPanel.SetActive (false);
	}

	public void launchPausePanel(){
		pausePanel.SetActive (true);
	}
	public void quitPausePanel(){
		pausePanel.SetActive (false);
	}

	public void launchGameOverPanel(){
		gameOverPanel.SetActive (true);
		Cursor.visible = true;
	}

	public void updateBossLife(float curPv, float maxPv){
		bossPanel.GetComponent<Slider>().value = ((float)curPv / (float)maxPv) ;
	}
		
	public void quitGame(){
		Application.Quit ();
	}

	public void Restart(){
		SceneManager.LoadScene (Application.loadedLevel);
	}

	public void resumeGame(){
		GameObject.Find ("GameManager").GetComponent<GameManager> ().resumeGame ();
	}


		
}
