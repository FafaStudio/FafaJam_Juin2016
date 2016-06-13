using UnityEngine;
using System.Collections;

public class Ombre : MonoBehaviour {

	public float posX;
	public float posY;

	public bool isDezoomed = false;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!isDezoomed)
			this.transform.position = new Vector2 (this.transform.parent.transform.position.x + posX, posY);
		else
			this.transform.position = new Vector2 (this.transform.parent.transform.position.x + posX,this.transform.parent.transform.position.y-1.2f);
	}

	/*void OnDezoom(){
		this.transform.position = new Vector2 (this.transform.parent.transform.position.x +, posY);
	}

	void OnZoom(){
	}*/
}
