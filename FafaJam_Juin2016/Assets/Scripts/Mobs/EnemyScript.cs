using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public int pv;
	public int scoreValue;
	protected GameObject target;
    public string enemyName;
	protected bool isDead = false;

	public Transform fumeParticle;

	//protected GameManager manager;

	void Awake(){
		target = GameObject.FindGameObjectWithTag ("Player");
		//manager = GameObject.Find ("GameManager").GetComponent<GameManager>();
	}

	public virtual void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "TIRPlayer") {
			this.pv -= 1;
			Destroy (coll.gameObject);
            StartCoroutine(this.GetComponent<HitColorChange>().launchHit());

            if (this.pv == 0) {
				//manager.updateScore (scoreValue);
				StartCoroutine (startDeath ());
			}
		}
	}

	public virtual IEnumerator startDeath(){
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreManager>().addScore(enemyName);
		isDead = true;
		this.GetComponent<SpriteRenderer> ().sprite = null;
		if (this.GetComponent<Animator> () != null) {
			this.GetComponent<Animator> ().Stop ();
		}
		this.GetComponent<Explosion> ().launchExplosion (this.gameObject);
        yield return new WaitForSeconds (0.2f);
		var puTransform = Instantiate (fumeParticle) as Transform;
		puTransform.position = this.transform.position;
		yield return new WaitForSeconds (0.2f);
		Destroy (this.gameObject);
	}


		
	void OnPauseGame(){
		this.enabled = false;
	}

	void OnResumeGame(){
		this.enabled = true;
	}
}
