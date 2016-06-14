using UnityEngine;
using System.Collections;

public class Baleine : EnemyScript {

	public GameObject bubbleEffect;
	private float timeBetweenBuble;

	private Animator animManager;

	public float cptBaleine = 10f;
	public float cptTimeBetweenAttack = 3f;
	public bool isSwitch = false;


	public bool isStopped = false;
	public bool launchSecondSalve = false;

	public Transform rocket;

	void Start () {
        enemyName = "Baleine";
		animManager = this.GetComponent<Animator> ();
	}
	

	void Update () {
		if (isStopped)
			return;
		if (cptBaleine <= 0) {
			float choice = Random.Range (0f, 1f);
			if (choice <= 0.65f) {
				if (isSwitch) {
					launchSecondSalve = true;
					StartCoroutine (launchRocket ());
				}else
					StartCoroutine (launchRocket ());
			} else {
					StartCoroutine (launchBlobyFish ());
			}
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
			if ((this.pv <= 100)&&(!isSwitch)) {
				StartCoroutine (switchPosition ());
			}
			
			if (this.pv <= 0) {
				StartCoroutine (launchDeath ());
			}
			StartCoroutine (this.GetComponent<HitColorChange>().launchHit());
        }
	}

	public IEnumerator switchPosition(){
		isStopped = true;
		this.GetComponent<CircleCollider2D> ().enabled = false;
		animManager.SetTrigger ("switchPosition");
		yield return new WaitForSeconds (6f);
		this.GetComponent<CircleCollider2D> ().enabled = true;
		this.GetComponent<CircleCollider2D> ().offset = new Vector2 (0f, 0f);
		cptTimeBetweenAttack = 1f;
		isSwitch = true;
		isStopped = false;
	}

	public IEnumerator launchDeath(){
		this.GetComponent<CircleCollider2D> ().enabled = false;
		animManager.SetTrigger ("isDead");
		yield return new WaitForSeconds (3f);
		StartCoroutine (GameObject.Find ("GameManager").GetComponent<GameManager> ().returnFromBossSequence ());
		Destroy (this.gameObject);
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
		cptBaleine = cptTimeBetweenAttack;
		isStopped = false;
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
		yield return new WaitForSeconds (0.5f);
		if (launchSecondSalve) {
			launchSecondSalve = false;
			StartCoroutine (launchRocket ());
		}
	}

	public void launchRocketSerie(){
		int cpt = 2;
		int randomStart = Random.Range (0, 99);
		bool isTroue = false;
		float firstTroue = -1f;
		bool troueDone = false;
		if (!isSwitch) {
			if (randomStart > 49) {
				for (int i = 0; i < 6; i++) {
					firstTroue = Random.Range (0f, 1f);
					if((i ==4) && (!troueDone) &&(!isTroue)){
						cptBaleine = cptTimeBetweenAttack;
						isStopped = false;
						return;
					}
					if (!isTroue) {
						if ((firstTroue > 0.5f) && (!troueDone)) {
							isTroue = true;
						} else {
							var puTransform = Instantiate (rocket) as Transform;
							puTransform.transform.position = new Vector2 (1f - (cpt * i), (10f + i));
						}
					} else {
						troueDone = true;
						isTroue = false;
					}
				}
			} else {
				for (int i = 0; i < 6; i++) {
					firstTroue = Random.Range (0f, 1f);
					if (!isTroue) {
						if ((firstTroue > 0.5f) && (!troueDone)) {
							isTroue = true;
						} else {
							var puTransform = Instantiate (rocket) as Transform;
							puTransform.transform.position = new Vector2 (-10f + (cpt * i), (10f + i));
						}
					} else {
						troueDone = true;
						isTroue = false;
					}
				}
			}
		} else {
			if (randomStart > 49) {
				for (int i = 0; i < 6; i++) {
					firstTroue = Random.Range (0f, 1f);
					if((i ==4) && (!troueDone) &&(!isTroue)){
						cptBaleine = cptTimeBetweenAttack;
						isStopped = false;
						return;
					}
					if (!isTroue) {
						if ((firstTroue > 0.5f) && (!troueDone)) {
							isTroue = true;
						} else {
							var puTransform = Instantiate (rocket) as Transform;
							puTransform.transform.position = new Vector2 ((cpt * i), (10f + i));
						}
					} else {
						troueDone = true;
						isTroue = false;
					}
				}
			} else {
				for (int i = 0; i < 6; i++) {
					firstTroue = Random.Range (0f, 1f);
					if (!isTroue) {
						if ((firstTroue > 0.5f) && (!troueDone)) {
							isTroue = true;
						} else {
							var puTransform = Instantiate (rocket) as Transform;
							puTransform.transform.position = new Vector2 ((cpt * i), (10f + i));
						}
					} else {
						troueDone = true;
						isTroue = false;
					}
				}
			}
		}
		cptBaleine = cptTimeBetweenAttack;
		isStopped = false;
	}


}
