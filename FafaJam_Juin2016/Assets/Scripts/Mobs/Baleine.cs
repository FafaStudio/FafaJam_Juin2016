using UnityEngine;
using System.Collections;

public class Baleine : EnemyScript {

	public GameObject bubbleEffect;
	private float timeBetweenBuble;

	private Animator animManager;

	public float cptBaleine = 5f;


	public bool isStopped = false;

	void Start () {
        enemyName = "Baleine";
		animManager = this.GetComponent<Animator> ();
	}
	

	void Update () {
		if (isStopped)
			return;
		if (cptBaleine <= 0) {
			//launchBloby ();
			StartCoroutine(launchBlobyFish());
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
                getKilled();
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


}
