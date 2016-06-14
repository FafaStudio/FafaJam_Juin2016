using UnityEngine;
using System.Collections;

public class SpawnerChief : MonoBehaviour {

	private SpawnerManager[] spawnerMobs;
	// 0, 1: les basiques oranges
	// 2 : blob fish
	// 3 : raie droite
	// 4 : raie gauche
	//5 : vive

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
		int choice = Random.Range (0, 9);
		switch (choice) {
            case 0:
                spawnerMobs[0].setSequence(2, 1f);
                spawnerMobs[1].setSequence(2, 1f);
                break;
            case 1:
                spawnerMobs[0].setSequence(2, 0.5f);
                spawnerMobs[1].setSequence(2, 0.5f);
                break;
            case 2://blob tj a droite
                spawnerMobs[2].setSequence(2, 1f);
                spawnerMobs[0].setSequence (1, 0.5f);
			    spawnerMobs[1].setSequence (1, 0.5f);
			    break;
            case 3:
                spawnerMobs[0].setSequence(1, 0.5f);
                spawnerMobs[3].setSequence(1, 0.5f);
                spawnerMobs[5].setSequence(1, 0.5f);
                break;
            case 4:
                spawnerMobs[3].setSequence(1, 0.5f);
                spawnerMobs[4].setSequence(1, 0.5f);
                spawnerMobs[5].setSequence(1, 0.5f);
                break;
            case 5:
                spawnerMobs[0].setSequence(1, 0.5f);
                spawnerMobs[2].setSequence(2, 1f);
                spawnerMobs[4].setSequence(1, 0.5f);
                break;
            case 6:
                spawnerMobs[0].setSequence(1, 0.5f);
                spawnerMobs[1].setSequence(1, 0.5f);
                spawnerMobs[2].setSequence(1, 0.5f);
                spawnerMobs[4].setSequence(1, 0.5f);
                spawnerMobs[5].setSequence(1, 0.5f);
                break;
            case 7:
                spawnerMobs[2].setSequence(1, 0.5f);
                spawnerMobs[4].setSequence(1, 0.5f);
                spawnerMobs[5].setSequence(1, 0.5f);
                break;
            case 8:
                spawnerMobs[0].setSequence(2, 1f);
                spawnerMobs[5].setSequence(3, 0.7f);
                break;
            case 9:
                spawnerMobs[1].setSequence(2, 1f);
                spawnerMobs[2].setSequence(2, 1f);
                spawnerMobs[5].setSequence(1, 0.5f);
                break;

                // 0, 1: les basiques oranges gauche droite;
                // 2 : blob fish
                // 3 : raie droite
                // 4 : raie gauche
                //5 : vive





                /*case 3:
                    spawnerMobs [0].setSequence (10, 1f);
                    spawnerMobs [1].setSequence (3, 2f);
                    spawnerMobs [2].setSequence (3, Random.Range (2f, 4f));
                    spawnerMobs [3].setSequence (1, Random.Range (2f, 4f));
                    spawnerMobs [4].setSequence (1, Random.Range (2f, 4f));
                    spawnerMobs [5].setSequence (1, Random.Range (2f, 4f));
                    break;*/
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
