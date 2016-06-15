using UnityEngine;
using System.Collections;

public class Baleine : EnemyScript {

	public GameObject bubbleEffect;
	private float timeBetweenBuble;

	private Animator animManager;

	public float cptBaleine = 10f;
	public float cptTimeBetweenAttack = 4f;
	public bool isSwitch = false;

	public AudioClip fompSound;
	public AudioClip screamSound;


	public bool isStopped = false;
	public bool launchSecondSalve = false;

	public bool canAction = true;

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
			takeDamage (1);
			Destroy (coll.gameObject);
		}
	}

	public void takeDamage(int degats){
		this.pv -= degats;
		GameObject.Find ("UI_Canvas").GetComponent<UIGameManager> ().updateBossLife ((float)this.pv, 300f);
		if ((this.pv <= 150)&&(!isSwitch)) {
			StartCoroutine (switchPosition ());
		}

		if (this.pv <= 0) {
			StartCoroutine (launchDeath ());
		}
		StartCoroutine (this.GetComponent<HitColorChange>().launchHit());
	}

	public bool canAttack(){
		return canAction;
	}
		

	public IEnumerator switchPosition(){
		canAction = false;
		isStopped = true;
		this.GetComponent<CircleCollider2D> ().enabled = false;
		animManager.SetTrigger ("switchPosition");
		AudioSource music = GameObject.Find ("GameManager").GetComponent<GameManager> ().sourceAudio;
		music.clip = GameObject.Find ("GameManager").GetComponent<GameManager> ().bossBaleineVener;
		music.Play ();
		music.loop = true;
		yield return new WaitForSeconds (9f);
		this.GetComponent<CircleCollider2D> ().enabled = true;
		this.GetComponent<CircleCollider2D> ().offset = new Vector2 (0f, 0f);
		cptTimeBetweenAttack = 2f;
		isSwitch = true;
		canAction = true;
		isStopped = false;
	}

	public IEnumerator launchDeath(){
		canAction = false;
		isStopped = true;
		this.GetComponent<CircleCollider2D> ().enabled = false;
		animManager.SetTrigger ("isDead");
		GameObject.Find ("MainCamera").GetComponent<CameraManager> ().shakeAmount = 0.1f;
		GameObject.Find ("MainCamera").GetComponent<CameraManager> ().setShake (3f);
		StartCoroutine (launchMultipleExplosion ());
		yield return new WaitForSeconds (3f);
		GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreManager>().addScore(enemyName);
		StartCoroutine (GameObject.Find ("GameManager").GetComponent<GameManager> ().returnFromBossSequence ());
		Destroy (this.gameObject);
	}

	public IEnumerator launchMultipleExplosion(){
		int nbExplosion = (int)Random.Range (10f, 20f);
		while (nbExplosion > 0) {
			nbExplosion--;
			this.GetComponent<Explosion> ().launchExplosion (this.gameObject, true);
			yield return new WaitForSeconds (0.2f);
		}
	}

	public IEnumerator launchBlobyFish(){
		isStopped = true;
		animManager.SetBool ("isBlobing", true);
		yield return new WaitForSeconds (0.2f);
		if (!canAttack ()){
			animManager.SetBool ("isBlobing", false);
			cptBaleine = cptTimeBetweenAttack;
			isStopped = false;
		}
		launchBubleParticle ();
		yield return new WaitForSeconds (1f);
		this.GetComponentInChildren<BaleineSpawner> ().setSequence (12, 0.5f);
		yield return new WaitForSeconds (7f);
		animManager.SetBool ("isBlobing", false);
		cptBaleine = cptTimeBetweenAttack;
		isStopped = false;
	}

	public IEnumerator launchRocket(){
		isStopped = true;
		for (int i = 0; i < 4; i++) {

			if (!canAttack ()) {
				cptBaleine = cptTimeBetweenAttack;
				isStopped = false;
				yield return null;
			}
			animManager.SetTrigger ("isFiring");
			this.GetComponent<AudioSource> ().clip = fompSound;
			this.GetComponent<AudioSource> ().Play ();
			yield return new WaitForSeconds (0.5f);
			if (!canAttack ()) {
				cptBaleine = cptTimeBetweenAttack;
				isStopped = false;
				yield return null;
			}
		}

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
					if (!canAttack ()){
						cptBaleine = cptTimeBetweenAttack;
						isStopped = false;
						return;
					}
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
					if (!canAttack ()){
						cptBaleine = cptTimeBetweenAttack;
						isStopped = false;
						return;
					}
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
					if (!canAttack ()){
						cptBaleine = cptTimeBetweenAttack;
						isStopped = false;
						return;
					}
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
					if (!canAttack ()){
						cptBaleine = cptTimeBetweenAttack;
						isStopped = false;
						return;
					}
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
