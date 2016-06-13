using UnityEngine;
using System.Collections;

public class OursinScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, 2f);
	}

	void Update () {
	
	}

	public void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Bouclier"){
			Destroy (this.gameObject);
		}
		if (col.gameObject.tag =="Player"){
			Destroy (this.gameObject);
		}
		if (col.gameObject.tag =="Sol"){
			Destroy (this.gameObject);
		}
	}
}
