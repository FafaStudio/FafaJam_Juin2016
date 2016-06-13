using UnityEngine;
using System.Collections;

public class CameraEffectDezoom : MonoBehaviour {

	public Camera mainCamera;
	public float sizeScreen;
	public GameObject decors;//reposition (0,0)
	public GameObject hero;
	public float heroPosition;
	public GameObject spawnerMobs;//reposition (0,0)

	public bool isLaunch = false;
	public bool isDezoomed = false;


	void Start () {
		sizeScreen = mainCamera.orthographicSize;
		heroPosition = hero.transform.position.y;
	}

	void Update () {
		if (!isLaunch)
			return;
		if (!isDezoomed) {
			Dezoom ();
		} else
			Zoom ();
	}

	public void Dezoom(){
		bool isGood1 = false, isGood2 = false, isGood3 = false;
		if (mainCamera.orthographicSize < sizeScreen * 1.25f) {
			mainCamera.orthographicSize += Time.deltaTime;
		} else
			isGood1 = true;
		if (decors.transform.position.y > -1.4f) {
			decors.transform.position = new Vector2 (decors.transform.position.x, decors.transform.position.y - 0.1f);
			spawnerMobs.transform.position = new Vector2( spawnerMobs.transform.position.x, spawnerMobs.transform.position.y-0.1f);
		} else
			isGood2 = true;
		if (hero.transform.position.y > -5.5f) {
			hero.transform.position = new Vector2 (hero.transform.position.x, hero.transform.position.y - 0.1f);
		} else {
			isGood3 = true;
			spawnerMobs.transform.position = new Vector2( spawnerMobs.transform.position.x, -2f);
		}
		if (isGood1 && isGood2 && isGood3) {
			isLaunch = false;
			isDezoomed = true;
		}
		setOmbres(-1.2f);
	}

	public void Zoom(){
		bool isGood1 = false, isGood2 = false, isGood3 = false;
		if (mainCamera.orthographicSize > sizeScreen ) {
			mainCamera.orthographicSize -= 3*Time.deltaTime;
		} else
			isGood1 = true;
		if (decors.transform.position.y < 0f) {
			decors.transform.position = new Vector2 (decors.transform.position.x, decors.transform.position.y + 0.1f);
			spawnerMobs.transform.position = new Vector2( spawnerMobs.transform.position.x, spawnerMobs.transform.position.y+0.1f);
		} else
			isGood2 = true;
		if (hero.transform.position.y < -4f) {
			hero.transform.position = new Vector2 (hero.transform.position.x, hero.transform.position.y + 0.1f);
		} else {
			isGood3 = true;
			spawnerMobs.transform.position = new Vector2( spawnerMobs.transform.position.x, 0);
		}
		if (isGood1 && isGood2 && isGood3) {
			isLaunch = false;
			isDezoomed = false;
		}
		setOmbres(1.5f);
	}

	public void setOmbres(float value){
		foreach (GameObject ombre in GameObject.FindGameObjectsWithTag ("Ombre")) {
			ombre.transform.position = new Vector2 (ombre.transform.parent.transform.position.x, ombre.transform.parent.transform.position.y+value);
			if (value >= 0f) {
				ombre.GetComponent<Ombre> ().isDezoomed = false;
			}
			else
				ombre.GetComponent<Ombre> ().isDezoomed = true;
		}
		foreach (GameObject spawner in GameObject.FindGameObjectsWithTag ("Spawner")) {
			spawner.GetComponent<SpawnerManager> ().isDezoomed = isDezoomed;
		}
	}
		
}
