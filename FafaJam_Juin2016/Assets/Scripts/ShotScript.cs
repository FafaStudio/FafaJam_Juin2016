using UnityEngine;
using System.Collections;

public class ShotScript : MonoBehaviour {

	public int damage = 1;

	public bool isEnemyShot = false;

	private bool isTouching = false;

	private Vector2 savedVelocity;

	public int speedX = 0;
	public int speedY = 0;

	void Start()
	{
		savedVelocity = this.GetComponent<Rigidbody2D> ().velocity;
		Destroy (this.gameObject, 2f);
	}

	void Update(){
		if (!isTouching)
			return;
		Destroy (this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Bouclier") {
			Destroy (this.gameObject);
		}
	}

	void OnPauseGame(){
		savedVelocity = this.GetComponent<Rigidbody2D> ().velocity;
		this.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);
		this.enabled = false;
	}

	void OnResumeGame(){
		this.GetComponent<Rigidbody2D> ().velocity = savedVelocity;
		this.enabled = true;
	}
}
