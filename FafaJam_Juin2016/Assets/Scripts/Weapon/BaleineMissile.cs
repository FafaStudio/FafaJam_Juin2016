using UnityEngine;
using System.Collections;

public class BaleineMissile : MonoBehaviour {

	public Transform fumeParticle;

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Player") {
			StartCoroutine (startDeath ());
		}
		if (coll.gameObject.tag == "Sol") {
			StartCoroutine (startDeath ());
		}
		if (coll.gameObject.tag == "Bouclier") {
			StartCoroutine (startDeath ());
		}
	}

	public IEnumerator startDeath(){
		var puTransform = Instantiate (fumeParticle) as Transform;
		puTransform.position = this.transform.position;
		yield return new WaitForSeconds (0.1f);
		Destroy (this.gameObject);
	}

		
}
