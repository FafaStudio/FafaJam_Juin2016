using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	//VARIABLES______________________________________________________________________________________________________________

	public Vector2 speed = new Vector2(20f, 20f); 
	private float movementX = 10f;
	public float movementY = 10f;
    private bool isJumping = true;
    private float jumpDuration = 0.1f;
    private float jumpStart = 0f;
	public int maxPv;
	private int curPv;


    private bool swapped = false;
    private float swapTimer = 0f;
    private float swapDuration = 0.2f;

    public GameObject attacker;
    public GameObject defender;

	private Ombre[] ombres;

	//private WeaponManager[] weapon;


	private bool isDead = false;

	Rigidbody2D body;

	public CameraManager camera;


	public AudioClip musicPerso;
	public AudioClip zikMort;

	//AWAKE, START, UPDATE...______________________________________________________________________________________________
	void Awake(){
		camera = GameObject.FindWithTag ("MainCamera").GetComponent<CameraManager> ();
		//camera = GameObject.FindWithTag ("MainCamera").GetComponent<CameraManager> ();
	}
	void Start () {
		body = GetComponent<Rigidbody2D> ();
		ombres= this.GetComponentsInChildren<Ombre> ();
		setOmbre (true);
		curPv = maxPv;
	}

	void Update () {
		if (isDead)
			return;
		calculMovement ();
        doSwap();
        //shoot ();
    }

	void FixedUpdate(){
		doMovement ();
	}


	//MOVEMENT________________________________________________________________________________________________________________

	private void calculMovement(){
		if (Input.GetKey (KeyCode.D)) {//mouvements latéraux
			speed.x = movementX;
			attacker.GetComponent<AttackerManager>().setAnimation ("RightButton", true);
			attacker.GetComponent<AttackerManager>().setAnimation ("LeftButton", false);
		} else if (Input.GetKey (KeyCode.Q)) {
			speed.x = -movementX;
			attacker.GetComponent<AttackerManager>().setAnimation ("RightButton", false);
			attacker.GetComponent<AttackerManager>().setAnimation ("LeftButton", true);
		} else {
			attacker.GetComponent<AttackerManager>().setAnimation ("RightButton", false);
			attacker.GetComponent<AttackerManager>().setAnimation ("LeftButton", false);
			speed.x = 0;
		}

        if ((Input.GetKey(KeyCode.Space) && !isJumping))//Jump
        {
            speed.y = movementY;
            isJumping = true;
            jumpStart = Time.time;
			attacker.GetComponent<AttackerManager>().setAnimation ("isJumping", true);
			setOmbre (false);
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

	private void setOmbre(bool value){
		for (int i = 0; i < ombres.Length; i++) {
			ombres [i].gameObject.SetActive (value);
		}
	}

    //SWAP POSITION______________________________________________________________________________________________________________

    public bool getSwapPosition()
    {
        return swapped;
    }
    private void doSwap()
    {
        if (Input.GetKey(KeyCode.E) && Time.time - swapTimer > swapDuration)
        {
            swap();
            swapTimer = Time.time;
        }
    }
    public void swap()
    {
        if (swapped)
        {
            Vector3 attackerActualPosition = attacker.transform.position;
            attacker.transform.position = defender.transform.position;
            defender.transform.position = attackerActualPosition;
			attacker.transform.localScale = new Vector2 (1, 1);
			defender.transform.localScale = new Vector2(1, 1);
			ombres[1].posY = -4.4f;

			attacker.GetComponent<AttackerManager> ().arms [0].GetComponent<Animator> ().SetBool ("isRight", true);
			attacker.GetComponent<AttackerManager> ().setAnimation ("isFront", true);
            swapped = false;
        }
        else
        {
            Vector3 attackerActualPosition = attacker.transform.position;
            attacker.transform.position = defender.transform.position;
            defender.transform.position = attackerActualPosition;
			attacker.transform.localScale = new Vector2 (-1, 1);
			defender.transform.localScale = new Vector2(-1, 1);

			ombres[1].posY = -4.2f;

			attacker.GetComponent<AttackerManager> ().arms [0].GetComponent<Animator> ().SetBool ("isRight", false);
			attacker.GetComponent<AttackerManager> ().setAnimation ("isFront", false);
            swapped = true;
        }
    }
    

    

	//GESTION VIE________________________________________________________________________________________________________________

    public int getPv()
    {
        return this.curPv;
    }

	public void takeDamage(int nbr)
	{
		this.curPv -= nbr;
		camera.setShake (0.3f);
		testEstMort ();
	}

    public void pvRegen(int nbr)
    {
        this.curPv += nbr;
        if (this.curPv > this.maxPv)
        {
            this.curPv = this.maxPv;
        }
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
			attacker.GetComponent<AttackerManager>().setAnimation ("isJumping", false);
			setOmbre (true);
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
