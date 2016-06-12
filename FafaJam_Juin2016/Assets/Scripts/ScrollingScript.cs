using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScrollingScript : MonoBehaviour
{
	public Vector2 speed = new Vector2(10, 10);
	private Vector2 direction = new Vector2(-1, 0);
	private Vector2 movement;
	private float savedSpeed;
	private Rigidbody2D body;

	public void Awake(){
		body = this.GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		movement = new Vector2(speed.x * direction.x, 0f);
	}

	void FixedUpdate()
	{
		body.velocity = movement;
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.name == "EndScrolling") {
			this.transform.position = new Vector2(GameObject.Find ("StartScrolling").transform.position.x, this.transform.position.y);
		}
	}

	void OnPauseGame(){
		savedSpeed = speed.x;
		speed.x = 0f;
	}

	void OnResumeGame(){
		speed.x = savedSpeed;
	}
}
