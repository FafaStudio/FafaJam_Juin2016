using UnityEngine;
using System.Collections;

public class BubleParticle : MonoBehaviour {

	public float timeBeforeDead = 0;

	void Start () {
		if (timeBeforeDead == 0f)
			Destroy (this.gameObject, 5f);
		else
			Destroy (this.gameObject, timeBeforeDead);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
