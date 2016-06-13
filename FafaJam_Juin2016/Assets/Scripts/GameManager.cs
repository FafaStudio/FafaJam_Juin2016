using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Vector2 mouse;
	public int w  = 32;
	public int h = 32;
	public Texture2D cursorTexture;

	public bool isPaused = false;

	public Transform baleine;

	public bool launchtest = false;

	public SpawnerChief spawners;

	void Start()
	{
		//Cursor.visible = false;
	}

	void Update()
	{
		mouse = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);


		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (isPaused)
				resumeGame ();
			else
				launchPaused ();
		}
		if (!launchtest)
			return;
		launchtest = false;
		StartCoroutine(launchBossSequence());
	}

	void OnGUI()
	{
		if (isPaused)
			return;
		GUI.DrawTexture(new Rect(mouse.x - (w / 2), mouse.y - (h / 2), w, h), cursorTexture);
	}

	public IEnumerator launchBossSequence(){
		this.GetComponent<CameraEffectDezoom> ().isLaunch = true;
		spawners.pauseSpawn = true;
		GameObject.Find ("MainCamera").GetComponent<CameraManager> ().setShake (3.5f);
		yield return new WaitForSeconds (1f);
		var puTransform = Instantiate(baleine) as Transform;
	}

	public IEnumerator returnFromBossSequence(){
		this.GetComponent<CameraEffectDezoom> ().isLaunch = true;
		spawners.pauseSpawn = false;
		yield return new WaitForSeconds (1f);
	}
		

	//PAUSE_____________________________________________________________________________________________________________

	public void launchPaused(){
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
