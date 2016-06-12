using UnityEngine;


public class GrenadeMovementScript : MonoBehaviour
{

	public Vector2 speed;

	public float savedSpeedX;
	public float savedSpeedY;

	public Vector2 direction;

	private Vector2 movement;

	private Rigidbody2D body;

	public bool isStopped = false;

    public float launchDuration;

	public void Awake(){
		body = this.GetComponent<Rigidbody2D>();
        launchDuration = 1f;
	}

	void Update()
	{
		if (!isStopped) {
            launchDuration -= Time.deltaTime;
            movement = new Vector2(speed.x * direction.x, speed.y*launchDuration * direction.y);
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