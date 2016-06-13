using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public int pv;
	public int scoreValue;
	protected GameObject target;

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
			if (this.pv <= 0) {
				//manager.updateScore (scoreValue);
				StartCoroutine(startDeath());
			}
		}
	}

	public IEnumerator startDeath(){
		var puTransform = Instantiate (fumeParticle) as Transform;
		puTransform.position = this.transform.position;
		yield return new WaitForSeconds (0.1f);
		Destroy (this.gameObject);
	}
		
	void OnPauseGame(){
		this.enabled = false;
	}

	void OnResumeGame(){
		this.enabled = true;
	}
}
