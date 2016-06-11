using UnityEngine;


public class MovementScript : MonoBehaviour
{

	public Vector2 speed;

	public float savedSpeedX;
	public float savedSpeedY;

	public Vector2 direction;

	private Vector2 movement;

	private Rigidbody2D body;

	public bool isStopped = false;

	public void Awake(){
		body = this.GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (!isStopped) {
			movement = new Vector2 (speed.x * direction.x, speed.y * direction.y);
		}
		else
			movement = new Vector2 (0, 0);
	}

	void FixedUpdate()
	{
		body.velocity = movement;
	}

	public void setDirection(int x, int y){
		direction.x = x;
		direction.y = y;
	}

	void OnPauseGame(){
		savedSpeedX = speed.x;
		savedSpeedY = speed.y;
		speed.x = 0f;
		speed.y = 0f;
	}

	void OnResumeGame(){
		speed.x = savedSpeedX;
		speed.y = savedSpeedY;
	}
}