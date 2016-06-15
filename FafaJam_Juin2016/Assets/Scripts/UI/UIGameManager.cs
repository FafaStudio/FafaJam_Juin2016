using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameManager : MonoBehaviour {

	public GameObject pausePanel;
    public GameObject gameOverPanel;
	public GameObject howToPanel;

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

	public void launchHowToPanel(){
		howToPanel.SetActive (true);
	}
	public void quitHowToPanel(){
		howToPanel.SetActive (false);
	}

	public void launchGameOverPanel(){
		gameOverPanel.SetActive (true);
		Cursor.visible = true;
	}

    public void displayStats()
    {
        gameOverPanel.transform.Find("BullePanel/BoutonsGameOver").gameObject.SetActive(false);
        gameOverPanel.transform.Find("BullePanel/PlusInfoButton").gameObject.SetActive(false);
        gameOverPanel.transform.Find("BullePanel/StatsContainer").gameObject.SetActive(true);
        gameOverPanel.transform.Find("BullePanel/MinusInfoButton").gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("ScoreDisplayer").GetComponent<ScoreDisplay>().displayScores();
    }

    public void unDisplayStats()
    {
        GameObject.FindGameObjectWithTag("ScoreDisplayer").GetComponent<ScoreDisplay>().unDisplayScores();
        gameOverPanel.transform.Find("BullePanel/StatsContainer").gameObject.SetActive(false);
        gameOverPanel.transform.Find("BullePanel/MinusInfoButton").gameObject.SetActive(false);
        gameOverPanel.transform.Find("BullePanel/BoutonsGameOver").gameObject.SetActive(true);
        gameOverPanel.transform.Find("BullePanel/PlusInfoButton").gameObject.SetActive(true);
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
