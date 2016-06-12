using UnityEngine;
using System.Collections;

public class Ombre : MonoBehaviour {

	public float posX;
	public float posY;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector2 (this.transform.parent.transform.position.x + posX, posY);
	}
}
