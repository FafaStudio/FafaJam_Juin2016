using UnityEngine;
using System.Collections;

public class vive : MonoBehaviour {

	public bool hasSwapPlayer = false;

	public Transform elecEffect;

	void Start () {
	
	}

	void Update () {
	
	}

	public void launchElecParticle(GameObject cible){
		var pu = Instantiate (elecEffect, new Vector3 (cible.transform.position.x , cible.transform.position.y ), Quaternion.identity) as Transform;
		//pu.SetParent (cible.transform);
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (hasSwapPlayer)
			return;
		if((coll.gameObject.name == "Attacker")){
			launchElecParticle (coll.gameObject);
			coll.gameObject.GetComponent<AttackerManager>().launchHitAnim();
			coll.gameObject.GetComponentInParent<PlayerManager> ().swap ();
			hasSwapPlayer = true;
		}
		if((coll.gameObject.name == "Defender")){
			launchElecParticle (coll.gameObject);
			//coll.gameObject.GetComponent<AttackerManager>().launchHitAnim();
			coll.gameObject.GetComponentInParent<PlayerManager> ().swap ();
			hasSwapPlayer = true;
		}
	}
}
