using UnityEngine;
using System.Collections;

public class SpawnerManager : MonoBehaviour {

	public Transform powerUp;
	public Transform enemyBasic;

	protected float chronoPowerUp;

	public int maxCpt = 4;
	public int cpt = 0;
	public bool isActiveMob = false;

	protected float chronoBasicEnemy;
	public float setterChronoEnemy = 0f;

	public bool isLeftSpawner;
	public bool isGroundSpawner = true;

	public bool isDezoomed = false;

	protected GameObject spawnerBox;
	void Awake(){
		spawnerBox = new GameObject ("mobsInside");
		spawnerBox.transform.parent = spawnerBox.transform;
	}
	void Start () {
		chronoPowerUp = Random.Range (5f, 30f);
		chronoBasicEnemy = Random.Range (2f, 6f);
		chronoBasicEnemy = 1f;
		spawnerBox = new GameObject ("mobsInside");
		spawnerBox.transform.parent = spawnerBox.transform;
	}

	void Update () {
		/*chronoPowerUp -= Time.deltaTime;
		spawnPowerUp ();*/

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

	public virtual void spawnBasicEnnemy(){
		if (chronoBasicEnemy <= 0f) {
			if (spawnerBox.GetComponentsInChildren<Transform> ().Length > 5) {
				setSequence (0, 0);
			}
			var puTransform = Instantiate(enemyBasic) as Transform;
			Vector3 newPos = new Vector3 (this.transform.position.x, this.transform.position.y, 0f);
			if (isGroundSpawner) {
				if (isLeftSpawner) {
					puTransform.gameObject.GetComponent<BasicEnemy> ().setSideSpawned (false);
				} else
					puTransform.gameObject.GetComponent<BasicEnemy> ().setSideSpawned (true);
			} else {
				newPos = new Vector3 (this.transform.position.x, this.transform.position.y+Random.Range(-1f, 2f), 0f);
			}

			if (puTransform.gameObject.name == "BasicEnemy(Clone)") {
				puTransform.GetComponentInChildren<Ombre> ().isDezoomed = isDezoomed;
			}
			
			puTransform.position = newPos;
			puTransform.SetParent (spawnerBox.transform);

			chronoBasicEnemy = setterChronoEnemy;
			cpt--;
			if (cpt <= 0) {
				isActiveMob = false;
			}
			
		}
	}

	public void setSequence(int compteur, float timeBetweenInstance){
		if (spawnerBox.GetComponentsInChildren<Transform> () != null) {
			if (spawnerBox.GetComponentsInChildren<Transform> ().Length > 5) {
				isActiveMob = false;
				return;
			}
		}
		this.maxCpt = compteur;
		cpt = maxCpt;
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
