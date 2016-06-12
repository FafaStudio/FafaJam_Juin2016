using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public int pv;
	public int scoreValue;
	protected GameObject target;

	//protected GameManager manager;

	void Awake(){
		target = GameObject.FindGameObjectWithTag ("Player");
		//manager = GameObject.Find ("GameManager").GetComponent<GameManager>();
	}

	public virtual void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "TIRPlayer") {
			this.pv -= 1;
			//StartCoroutine (coll.gameObject.GetComponent<ShotScript> ().startEnd ());
			if (this.pv <= 0) {
				//manager.updateScore (scoreValue);
				Destroy (this.gameObject);
			}
		}
	}
		
	void OnPauseGame(){
		this.enabled = false;
	}

	void OnResumeGame(){
		this.enabled = true;
	}
}
