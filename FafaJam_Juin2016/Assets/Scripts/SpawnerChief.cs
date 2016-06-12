using UnityEngine;
using System.Collections;

public class SpawnerChief : MonoBehaviour {

	private SpawnerManager[] spawnerSol;

	public float maxCompteurWave = 5f;
	private float compteur;

	public bool playerIsDead = false;

	void Start () {
		spawnerSol = this.GetComponentsInChildren<SpawnerManager> ();
		compteur = 2f;
	}


	void Update () {
		if (playerIsDead)
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
			spawnerSol[0].setSequence (5, 2f);
			spawnerSol[1].setSequence (3, 2f);
			spawnerSol[2].setSequence (3, 2f);
			break;
		case 1:
			spawnerSol[0].setSequence (10, 3f);
			spawnerSol[1].setSequence (3, 2f);
			spawnerSol[2].setSequence (3, 2f);
			break;
		case 2:
			spawnerSol[0].setSequence (10, 3f);
			spawnerSol[1].setSequence (4, 2f);
			spawnerSol[2].setSequence (3, 2f);
			break;
		case 3:
			spawnerSol[0].setSequence (10, 1f);
			spawnerSol[1].setSequence (3, 2f);
			spawnerSol[2].setSequence (3, 2f);
			break;
		case 4:
			spawnerSol[0].setSequence (3, 3f);
			spawnerSol[1].setSequence (3, 2f);
			spawnerSol[2].setSequence (3, 2f);
			break;
		case 5:
			spawnerSol[0].setSequence (5, 2f);
			spawnerSol[1].setSequence (8, 1f);
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
