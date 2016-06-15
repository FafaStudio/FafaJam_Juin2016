using UnityEngine;
using System.Collections;

public class splashImage : MonoBehaviour {

	public float timer = 3f;

	void Start () {

	}

	// Update is called once per frame
	void Update () {

		timer -= Time.deltaTime;
		if (timer <= 0) {
			Application.LoadLevel("MainMenu");
		}

	}
}
