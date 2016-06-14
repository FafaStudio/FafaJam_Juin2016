using UnityEngine;
using System.Collections;

public class ShotScript : MonoBehaviour {

	public int damage = 1;

	public bool isEnemyShot = false;

	protected bool isTouching = false;

	private Vector2 savedVelocity;

	public int speedX = 0;
	public int speedY = 0;

	public virtual void Start()
	{
		savedVelocity = this.GetComponent<Rigidbody2D> ().velocity;
		Destroy (this.gameObject, 2f);
	}

	void Update(){
		if (!isTouching)
			return;
		Destroy (this.gameObject);
	}

	public virtual void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Bouclier") {
			Vector2 ricoche = new Vector2 (-this.GetComponent<MovementScript> ().direction.x, Random.Range (0.5f, 6f));
			this.GetComponent<MovementScript>().direction = ricoche;
			this.transform.rotation = Quaternion.Euler (0f, 0f, (Mathf.Atan2 ((ricoche.y), (ricoche.x)) * Mathf.Rad2Deg));
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
