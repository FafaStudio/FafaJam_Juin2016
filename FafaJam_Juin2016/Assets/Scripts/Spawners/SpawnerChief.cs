using UnityEngine;
using System.Collections;

public class SpawnerChief : MonoBehaviour {

	private SpawnerManager[] spawnerMobs;
	// 0, 1: les basiques oranges
	// 2 : blob fish
	// 3 : raie droite
	// 4 : raie gauche

	public float maxCompteurWave = 5f;
	private float compteur;

	public bool playerIsDead = false;
	public bool pauseSpawn = false;

	void Start () {
		spawnerMobs = this.GetComponentsInChildren<SpawnerManager> ();
		compteur = 2f;
	}


	void Update () {
		if ((playerIsDead)||(pauseSpawn))
			return;
		compteur -= Time.deltaTime;
		if (compteur <= 0) {
			launchWaves ();
			compteur = maxCompteurWave;
		}
	}

	public void launchWaves(){
		int choice = Random.Range (0, 5);
		switch (choice) {
		case 0:
			spawnerMobs[0].setSequence (5, 2f);
			spawnerMobs[1].setSequence (3, 2f);
			spawnerMobs[2].setSequence (3, 2f);
			spawnerMobs[3].setSequence (1, 2f);
			spawnerMobs[4].setSequence (1, 2f);
			break;
		case 1:
			spawnerMobs[0].setSequence (10, 3f);
			spawnerMobs[1].setSequence (3, 2f);
			spawnerMobs[2].setSequence (3, 2f);
			spawnerMobs[3].setSequence (1, 2f);
			spawnerMobs[4].setSequence (1, 2f);
			break;
		case 2:
			spawnerMobs[0].setSequence (10, 3f);
			spawnerMobs[1].setSequence (4, 2f);
			spawnerMobs[2].setSequence (3, 2f);
			spawnerMobs[3].setSequence (1, 2f);
			spawnerMobs[4].setSequence (1, 2f);
			break;
		case 3:
			spawnerMobs[0].setSequence (10, 1f);
			spawnerMobs[1].setSequence (3, 2f);
			spawnerMobs[2].setSequence (3, 2f);
			spawnerMobs[3].setSequence (1, 2f);
			spawnerMobs[4].setSequence (1, 2f);
			break;
		case 4:
			spawnerMobs[0].setSequence (3, 3f);
			spawnerMobs[1].setSequence (3, 2f);
			spawnerMobs[2].setSequence (3, 2f);
			spawnerMobs[3].setSequence (1, 2f);
			spawnerMobs[4].setSequence (1, 2f);
			break;
		case 5:
			spawnerMobs[0].setSequence (5, 2f);
			spawnerMobs[1].setSequence (8, 1f);
			spawnerMobs[3].setSequence (1, 2f);
			spawnerMobs[4].setSequence (1, 2f);
			break;
		}
	}

	void OnPauseGame(){
		this.enabled = false;
		playerIsDead = true;
	}

	void OnResumeGame(){
		this.enabled = true;
		playerIsDead = false;
	}
}
