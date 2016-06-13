using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public int pv;
	public int scoreValue;
	protected GameObject target;
    public string enemyName;

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
			//StartCoroutine (coll.gameObject.GetComponent<ShotScript> ().startEnd ());
			if (this.pv <= 0) {
                //manager.updateScore (scoreValue);
                getKilled();
			}
		}
	}

    public void getKilled()
    {

        GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreManager>().addScore(enemyName);
        Destroy(this.gameObject);
    }
		
	void OnPauseGame(){
		this.enabled = false;
	}

	void OnResumeGame(){
		this.enabled = true;
	}
}
