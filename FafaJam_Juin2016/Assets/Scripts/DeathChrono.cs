using UnityEngine;
using System.Collections;

public class DeathChrono : MonoBehaviour {

	public float timeBeforeDeath;

	void Update () {
		if (timeBeforeDeath > 0)
			timeBeforeDeath -= Time.deltaTime;
		else
			Destroy (this.gameObject);
	}

	void OnPauseGame(){
		this.enabled = false;
	}

	void OnResumeGame(){
		this.enabled = true;
	}
}
