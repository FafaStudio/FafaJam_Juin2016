using UnityEngine;
using System.Collections;

public class SpawnerManager : MonoBehaviour {

	public Transform powerUp;
	public Transform enemyBasic;

	private float chronoPowerUp;

	public int maxCpt = 4;
	public int cpt = 0;
	public bool isActiveMob = false;

	private float chronoBasicEnemy;
	public float setterChronoEnemy = 0f;

	void Start () {
		chronoPowerUp = Random.Range (5f, 30f);
		chronoBasicEnemy = Random.Range (2f, 6f);
	}

	void Update () {
		chronoPowerUp -= Time.deltaTime;
		spawnPowerUp ();

		if (!isActiveMob)
			return;
		chronoBasicEnemy -= Time.deltaTime;
		spawnBasicEnnemy ();

	}

	public void spawnPowerUp(){
		if (chronoPowerUp <= 0f) {
			var puTransform = Instantiate(powerUp) as Transform;
			Vector3 newPos = new Vector3 (this.transform.position.x, Random.Range (-3.5f, 3.5f), 0f);
			puTransform.position = newPos;
			puTransform.SetParent (this.transform);
			chronoPowerUp = Random.Range (15f, 25f);
		}
	}

	public void spawnBasicEnnemy(){
		if (chronoBasicEnemy <= 0f) {
			var puTransform = Instantiate(enemyBasic) as Transform;
			Vector3 newPos = new Vector3 (this.transform.position.x, Random.Range (-3.5f, 3.5f), 0f);
			puTransform.position = newPos;
			puTransform.SetParent (this.transform);

			chronoBasicEnemy = setterChronoEnemy;
			cpt--;
			if (cpt <= 0) {
				isActiveMob = false;
			}
		}
	}

	public void setSequence(int compteur, float timeBetweenInstance){
		this.maxCpt = compteur;
		this.setterChronoEnemy = timeBetweenInstance;
		this.chronoBasicEnemy = setterChronoEnemy;
		isActiveMob = true;
	}

	void OnPauseGame(){
		this.enabled = false;
	}

	void OnResumeGame(){
		this.enabled = true;
	}
}
