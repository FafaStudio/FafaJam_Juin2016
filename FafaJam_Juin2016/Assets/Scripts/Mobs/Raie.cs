using UnityEngine;
using System.Collections;

public class Raie : EnemyScript {

	public float cptSpawn = 1f;
	public float maxCpt = 1f;

	public Transform oursin;

	private bool stop = false;

	public GameObject bubbleEffect;
	private float timeBetweenBuble;

	void Start () {
        enemyName = "Raie";
		maxCpt = Random.Range (0.7f, 1.3f);
		cptSpawn = maxCpt;
		Destroy (this.gameObject, 10f);
		timeBetweenBuble = 0.15f;
	}
	

	void Update () {
		launchBubleParticle ();
		if (stop)
			return;
		cptSpawn -= Time.deltaTime;
		if (cptSpawn <= 0) {
			stop = true;
			StartCoroutine (launchOursin ());
			maxCpt = Random.Range (0.7f, 1.3f);
			cptSpawn = maxCpt;
		}
	}

	public void spawnBasicEnnemy(){
		Transform puTransform = Instantiate(oursin) as Transform;
		puTransform.position = new Vector3 (this.transform.position.x, this.transform.position.y, 0f);

	}

	public IEnumerator launchOursin(){
		spawnBasicEnnemy ();
		yield return new WaitForSeconds (0.3f);
		spawnBasicEnnemy ();
		yield return new WaitForSeconds (0.3f);
		spawnBasicEnnemy ();
		yield return new WaitForSeconds (0.3f);
		stop = false;
	}

	public void setDirection(int value){
		this.GetComponent<MovementScript> ().direction = new Vector2 (value, 0f);
		if (value == -1) {
			this.transform.localScale = new Vector2 (1f, 1f);
			this.transform.GetComponent<MovementScript> ().speed = new Vector2 (7f, 0f);
		} else {
			this.transform.localScale = new Vector2 (-1f, 1f);
			this.transform.GetComponent<MovementScript> ().speed = new Vector2 (5f, 0f);
		}
	}

	public void launchBubleParticle(){
		if (timeBetweenBuble > 0) {
			timeBetweenBuble -= Time.deltaTime;
		}
		/* else {
			if (isSpawnedRight) {//tireur a gauche
				Instantiate (bubbleEffect, new Vector3 (this.transform.position.x - 0.3f, this.transform.position.y + 1f), Quaternion.identity);
			}else
				Instantiate (bubbleEffect, new Vector3 (this.transform.position.x + 0.3f, this.transform.position.y + 1f), Quaternion.identity);*/
			else{
				Instantiate (bubbleEffect, new Vector3 (this.transform.position.x+1f , this.transform.position.y-0.2f ), Quaternion.identity);
			timeBetweenBuble = 0.15f;
			
		}
	}


	public override void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "TIRPlayer") {
			this.pv -= 1;
			Destroy (coll.gameObject);
			StartCoroutine (this.GetComponent<HitColorChange>().launchHit());
			if (this.pv <= 0) {
<<<<<<< HEAD
				//manager.updateScore (scoreValue);
				StartCoroutine(startDeath());
				Destroy (this.gameObject);
			}
		}
=======
                //manager.updateScore (scoreValue);
                getKilled();
            }
        }
>>>>>>> b61acf28f50c06853a68f726715ba0107915cf0c
	}

}
