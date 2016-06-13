using UnityEngine;
using System.Collections;

public class Baleine : EnemyScript {

	public GameObject bubbleEffect;
	private float timeBetweenBuble;

	private Animator animManager;

	public float cptBaleine = 5f;


	public bool isStopped = false;

	public Transform rocket;

	void Start () {
		animManager = this.GetComponent<Animator> ();
	}
	

	void Update () {
		if (isStopped)
			return;
		if (cptBaleine <= 0) {
			//launchBloby ();
			StartCoroutine(launchRocket());
		}else
			cptBaleine -= Time.deltaTime;
	}

	public void launchBubleParticle(){
		Instantiate (bubbleEffect, new Vector3 (this.transform.position.x-0.5f , this.transform.position.y + 1.2f), Quaternion.identity);
	}

	public override void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "TIRPlayer") {
			this.pv -= 1;
			Destroy (coll.gameObject);
			StartCoroutine (this.GetComponent<HitColorChange>().launchHit());
			if (this.pv <= 0) {
				//manager.updateScore (scoreValue);
				Destroy (this.gameObject);
			}
		}
	}

	public IEnumerator launchBlobyFish(){
		isStopped = true;
		animManager.SetBool ("isBlobing", true);
		yield return new WaitForSeconds (0.2f);
		launchBubleParticle ();
		yield return new WaitForSeconds (1f);
		this.GetComponentInChildren<BaleineSpawner> ().setSequence (15, 0.2f);
		yield return new WaitForSeconds (5.5f);
		animManager.SetBool ("isBlobing", false);
	}

	public IEnumerator launchRocket(){
		isStopped = true;
		animManager.SetTrigger ("isFiring");
		yield return new WaitForSeconds (0.5f);
		animManager.SetTrigger ("isFiring");
		yield return new WaitForSeconds (0.5f);
		animManager.SetTrigger ("isFiring");
		yield return new WaitForSeconds (0.5f);
		animManager.SetTrigger ("isFiring");
		yield return new WaitForSeconds (0.5f);
		launchRocketSerie ();
	}

	public void launchRocketSerie(){
		int cpt = 2;
		int randomStart = Random.Range (0, 99);
		bool isTroue = false;
		float firstTroue = -1f;
		bool troueDone = false;
		if (randomStart > 49) {
			for (int i = 0; i < 6; i++) {
				firstTroue = Random.Range (0f, 1f);
				if (!isTroue) {
					if ((firstTroue > 0.5f)&&(!troueDone)) {
						isTroue = true;
					} else {
						var puTransform = Instantiate (rocket) as Transform;
						puTransform.transform.position = new Vector2 (target.transform.position.x - (cpt * i), (10f + i ));
					}
				}else{
					troueDone = true;
					isTroue = false;
				}
			}
		} else {
			for (int i = 0; i < 6; i++) {
				firstTroue = Random.Range (0f, 1f);
				if (!isTroue) {
					if ((firstTroue > 0.5f)&&(!troueDone)) {
						isTroue = true;
					} else {
						var puTransform = Instantiate (rocket) as Transform;
						puTransform.transform.position = new Vector2 (-10f + (cpt * i), (10f + i ));
					}
				}else{
					troueDone = true;
					isTroue = false;
				}
			}
		}
	}


}
