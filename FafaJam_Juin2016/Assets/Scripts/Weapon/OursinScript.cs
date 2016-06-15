using UnityEngine;
using System.Collections;

public class OursinScript : MonoBehaviour {

	void Start () {
		Destroy (this.gameObject, 2f);
	}

	void OnTriggerEnter2D(Collider2D coll){
		if((coll.gameObject.name == "Attacker")){
			coll.gameObject.GetComponent<AttackerManager> ().takeDamage (1);
			coll.gameObject.GetComponentInParent<PlayerManager> ().swap ();
			Destroy (this.gameObject);
		}
		if((coll.gameObject.name == "Defender")){
			coll.gameObject.GetComponent<DefenderManager> ().takeDamage (1);
			coll.gameObject.GetComponentInParent<PlayerManager> ().swap ();
			Destroy (this.gameObject);
		}
		if (coll.gameObject.tag =="Sol"){
			Destroy (this.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Bouclier"){
			print ("bisous");
			Destroy (this.gameObject);
		}
		if (coll.gameObject.tag =="Player"){
			print ("bisous");
			coll.gameObject.GetComponent<AttackerManager> ().takeDamage (1);
			Destroy (this.gameObject);
		}
		if (coll.gameObject.tag =="Sol"){
			Destroy (this.gameObject);
		}
	}
}
