using UnityEngine;
using System.Collections;

public class Raie : EnemyScript {

	public float cptSpawn = 1f;
	public float maxCpt = 1f;

	public Transform oursin;

	private bool stop = false;

	void Start () {
        enemyName = "Raie";
		maxCpt = Random.Range (0.7f, 1.3f);
		cptSpawn = maxCpt;
	}
	

	void Update () {
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

	public override void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "TIRPlayer") {
			this.pv -= 1;
			Destroy (coll.gameObject);
			StartCoroutine (this.GetComponent<HitColorChange>().launchHit());
			if (this.pv <= 0) {
                //manager.updateScore (scoreValue);
                getKilled();
            }
        }
	}

}
