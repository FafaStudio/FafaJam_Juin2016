using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	//VARIABLES______________________________________________________________________________________________________________

	public Vector2 speed = new Vector2(20f, 20f); 
	private float movementX = 10f;
	private float movementY = 10f;
    public bool isJumping = true;
    public float jumpDuration = 0.1f;
    public float jumpStart = 0f;
	public int maxPv;
	private int curPv;

    private Animator animManager;

	//private WeaponManager[] weapon;

	public Vector2 movement;

	private bool isDead = false;

	Rigidbody2D body;

	private CameraManager camera;


	public AudioClip musicPerso;
	public AudioClip zikMort;

	//AWAKE, START, UPDATE...______________________________________________________________________________________________
	void Awake(){
		camera = GameObject.FindWithTag ("MainCamera").GetComponent<CameraManager> ();
		//camera = GameObject.FindWithTag ("MainCamera").GetComponent<CameraManager> ();
	}
	void Start () {
		animManager = this.GetComponent<Animator> ();
		body = GetComponent<Rigidbody2D> ();
		curPv = maxPv;
	}

	void Update () {
		if (isDead)
			return;
		calculMovement ();
        //shoot ();
    }

	void FixedUpdate(){
		doMovement ();
	}


	//MOVEMENT________________________________________________________________________________________________________________

	private void calculMovement(){
		if (Input.GetKey (KeyCode.D)) {//mouvements latéraux
			speed.x = movementX;
		} else if (Input.GetKey (KeyCode.Q)) {
			speed.x = -movementX;
		} else {
			speed.x = 0;
		}

        if ((Input.GetKey(KeyCode.Space) && !isJumping))//Jump
        {
            speed.y = movementY;
            isJumping = true;
            jumpStart = Time.time;
        }
        else if (jumpStart > 1f && Time.time - jumpStart < jumpDuration && isJumping)
        {
            speed.y = movementY;
        }
        else
        {
            speed.y = 0;
        }
    }


    private void doMovement(){
		body.velocity = speed;
	}

	//GESTION VIE________________________________________________________________________________________________________________

	public void takeDamage(int nbr)
	{
		this.curPv -= nbr;
		camera.setShake (0.3f);
		testEstMort ();
	}

	public void testEstMort()
	{
		if (this.curPv <= 0) {
			isDead =true;
		}
	}



	//COLLISION________________________________________________________________________________________________________________

	void OnTriggerEnter2D(Collider2D col){
	}


    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Sol" ){
            isJumping = false;
        }
    }


	public void shoot(){
		/*if (Input.GetKey (KeyCode.Space)) {
			animManager.SetBool ("Firing", true);
			if (weapon.Length != 0)
			{
				for (int i = 0; i < weapon.Length; i++) {
					weapon[i].AttackWithCustomPosition (this.levelHero);
				}
			}
		}
		if(Input.GetKeyUp(KeyCode.Space))
			animManager.SetBool("Firing", false);*/
	} 

	void OnPauseGame(){
		this.enabled = false;
	}

	void OnResumeGame(){
		this.enabled = true;
	}
}
