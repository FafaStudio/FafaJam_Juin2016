using UnityEngine;
using System.Collections;

public class ElecParticle : MonoBehaviour {
	private float decreaseFactor = 1.0f;
	private float shake = 0f;
	public float shakeAmount = 0.1f;

	void Start(){
		shake = 0.5f;
	}

	void Update() {
		if (shake > 0) {
			this.transform.position = Random.insideUnitSphere * shakeAmount + new Vector3 (this.transform.position.x, this.transform.position.y, 0f) ;
			shake -= Time.deltaTime * decreaseFactor;

		} else {
			shake = 0.0f;
		}
	}

	public void setShake(float value){
		shake = value;
	}
	
}

