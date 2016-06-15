using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Vector2 mouse;
	public int w  = 32;
	public int h = 32;
	public Texture2D cursorTexture;

	public bool isPaused = false;

	public Transform baleine;

	public bool launchBoss = false;

	public SpawnerChief spawners;

	public AudioClip bossBaleineMusic;
	public AudioClip bossBaleineVener;
	public AudioClip[] normalMusic;
	public AudioClip gameOverMusic;

	public AudioSource sourceAudio;

	public ScoreManager scoreManager;

	private int cptBoss = 1;
	public int scoreForBoss = 10000;

	public UIGameManager uiManager;

	void Start()
	{
		sourceAudio = this.GetComponent<AudioSource> ();
		scoreManager = this.GetComponent<ScoreManager> ();
		Cursor.visible = false;
		sourceAudio.clip = normalMusic[Random.Range(0, normalMusic.Length)];
		sourceAudio.Play();
	}

	void Update()
	{
		mouse = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);

		if (!sourceAudio.isPlaying)
		{
			if (launchBoss)
				return;
			sourceAudio.clip = normalMusic[Random.Range(0, normalMusic.Length)];
			sourceAudio.Play();
		}

		if (Input.GetButtonDown("Pause")) {
			if (isPaused)
				resumeGame ();
			else
				launchPaused ();
		}
		if((getScore()>=(scoreForBoss))){
			if (launchBoss)
				return;
			launchBoss = true;
			scoreForBoss += 20000;
			StartCoroutine (launchBossSequence ());
		}
	}

	void OnGUI()
	{
		if (isPaused)
			return;
		GUI.DrawTexture(new Rect(mouse.x - (w / 2), mouse.y - (h / 2), w, h), cursorTexture);
	}

	public float getScore(){
		return scoreManager.score;
	}

	public IEnumerator launchBossSequence(){
		this.GetComponent<CameraEffectDezoom> ().isLaunch = true;
		spawners.pauseSpawn = true;
		uiManager.launchBossPanel ();
		GameObject.Find ("MainCamera").GetComponent<CameraManager> ().shakeAmount = 0.1f;
		GameObject.Find ("MainCamera").GetComponent<CameraManager> ().setShake (3.5f);
		yield return new WaitForSeconds (1f);
		this.GetComponent<AudioSource> ().Stop ();
		var puTransform = Instantiate(baleine) as Transform;
		yield return new WaitForSeconds (2f);
		this.GetComponent<AudioSource> ().clip = bossBaleineMusic;
		this.GetComponent<AudioSource> ().Play ();
		this.GetComponent<AudioSource> ().loop = true;
	}

	public IEnumerator returnFromBossSequence(){
		cptBoss++;
		uiManager.stopBossPanel ();
		this.GetComponent<CameraEffectDezoom> ().isLaunch = true;
		launchBoss = false;
		spawners.pauseSpawn = false;
		sourceAudio.clip = normalMusic[Random.Range(0, normalMusic.Length)];
		sourceAudio.Play();
		this.GetComponent<AudioSource> ().loop = false;
		yield return new WaitForSeconds (1f);
	}
		

	//PAUSE_____________________________________________________________________________________________________________

	public void launchPaused(){
		sourceAudio.volume = 0.4f;
		uiManager.launchPausePanel ();
		isPaused = true;
		Cursor.visible = true;
		//musicLauncher.volume = 0.4f;
		//player.ui.pausePanel.SetActive (true);
		Object[] objects = FindObjectsOfType (typeof(GameObject));
		foreach (GameObject go in objects) {
			go.SendMessage ("OnPauseGame", SendMessageOptions.DontRequireReceiver);
		}
	}



	public void resumeGame(){
		sourceAudio.volume = 0.7f;
		uiManager.quitPausePanel ();
        isPaused = false;
		Cursor.visible = false;
		//musicLauncher.volume = 1f;
		//	player.ui.pausePanel.SetActive (false);
		Object[] objects = FindObjectsOfType (typeof(GameObject));
		foreach (GameObject go in objects) {
			go.SendMessage ("OnResumeGame", SendMessageOptions.DontRequireReceiver);
		}
	}
}
