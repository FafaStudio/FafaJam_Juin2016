using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

	//VARIABLES______________________________________________________________________________________________________________


    private bool defenderIsJumping = true;
    private int compteurDefenderJumpFrame = 0;

	public Vector2 speed = new Vector2(20f, 20f); 
	private float movementX = 10f;
	public float movementY = 20f;
    private bool isJumping = true;
    private float jumpDuration = 0.2f;
    private float jumpStart = 0f;

	public int maxPv;
	private int curPv;

	public Slider hpUI;
	public UIGameManager uiManager;


    private bool swapped = false;
    private float swapTimer = 0f;
    private float swapDuration = 0.2f;

    public GameObject attacker;
    public GameObject defender;

	private Ombre[] ombres;

	public bool isDead = false;

	public Transform fumeParticle;

	Rigidbody2D body;

	public CameraManager camera;

//AWAKE, START, UPDATE...______________________________________________________________________________________________

	void Awake(){
		curPv = maxPv;
		hpUI = GameObject.Find ("HpSlider").GetComponent<Slider> ();
		uiManager = GameObject.Find ("UI_Canvas").GetComponent<UIGameManager> ();
		camera = GameObject.FindWithTag ("MainCamera").GetComponent<CameraManager> ();
		hpUI.value = (curPv / maxPv) ;
	}

	void Start () {
		body = GetComponent<Rigidbody2D> ();
		ombres= this.GetComponentsInChildren<Ombre> ();
		setOmbre (true);
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
			attacker.GetComponent<AttackerManager> ().arms [0].GetComponent<Animator> ().SetBool ("isJumping", true);
			attacker.GetComponent<AttackerManager> ().arms [1].GetComponent<Animator> ().SetBool ("isJumping", true);
			setOmbre (false);
        }
        else if (jumpStart > 1f && Time.time - jumpStart < jumpDuration && isJumping)
        {
            //speed.y = movementY;

        }
        else
        {
            speed.y = 0;
        }
    }

	public IEnumerator startDeath(){
		isDead = true;
		this.enabled = false;
		Destroy (attacker);
		Destroy (defender);
		this.GetComponent<Explosion> ().launchExplosion (this.gameObject);
		uiManager.launchGameOverPanel ();
		uiManager.displayFinalScore (GameObject.Find ("GameManager").GetComponent<GameManager>().scoreManager.score);
		GameObject.Find ("GameManager").GetComponent<GameManager> ().isPaused = true;
		GameObject.Find ("GameManager").GetComponent<GameManager> ().sourceAudio.clip = GameObject.Find ("GameManager").GetComponent<GameManager> ().gameOverMusic;
		GameObject.Find ("GameManager").GetComponent<GameManager> ().sourceAudio.Play ();
		yield return new WaitForSeconds (0.2f);
		var puTransform = Instantiate (fumeParticle) as Transform;
		puTransform.position = this.transform.position;
		uiManager.launchGameOverPanel ();
		Destroy (this.gameObject);
	}


    private void doMovement(){
        if (isJumping)
        {
            doMovementAttacker(speed);
            if (speed.y != 0)
            {
                compteurDefenderJumpFrame++;
            }
            if(!defenderIsJumping && Time.time - jumpStart > 0.1f)
            {
                doMovementDefender(new Vector2(speed.x, movementY));
                compteurDefenderJumpFrame--;
                if(compteurDefenderJumpFrame == 0)
                {
                    defenderIsJumping = true;
                }
            }
            else
            {
                doMovementDefender(new Vector2(speed.x, 0f));
            }
        }
        else
        {
            doMovementAttacker(speed);
            doMovementDefender(speed);
        }
        
        if(Mathf.Abs(attacker.transform.position.x - defender.transform.position.x) != 1.52f)//maintient de l'écart entre les personnages en x  
        {
            if (swapped)
            {
                attacker.transform.position = new Vector3(attacker.transform.position.x + (defender.transform.position.x - attacker.transform.position.x - 1.52f)/2, attacker.transform.position.y, attacker.transform.position.z);
                defender.transform.position = new Vector3(defender.transform.position.x - (defender.transform.position.x - attacker.transform.position.x - 1.52f)/2, defender.transform.position.y, defender.transform.position.z);
            }
            else
            {
                attacker.transform.position = new Vector3(attacker.transform.position.x - (attacker.transform.position.x - defender.transform.position.x - 1.52f) / 2, attacker.transform.position.y, attacker.transform.position.z);
                defender.transform.position = new Vector3(defender.transform.position.x + (attacker.transform.position.x - defender.transform.position.x - 1.52f) / 2, defender.transform.position.y, defender.transform.position.z);
            }
        }
    }

    private void doMovementAttacker(Vector2 temp)
    {
        attacker.GetComponent<Rigidbody2D>().velocity = temp;
    }
    private void doMovementDefender(Vector2 temp)
    {
        defender.GetComponent<Rigidbody2D>().velocity = temp;
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
        if (Input.GetKey(KeyCode.E) && Time.time - swapTimer > swapDuration && !isJumping)
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
			ombres[1].posY = -5.2f;

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

			ombres[1].posY = -5f;

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
		uploadLife ();
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

	public void uploadLife(){
		hpUI.value = ((float)this.curPv / (float)this.maxPv) ;
	}

//COLLISION________________________________________________________________________________________________________________

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Sol" ){
            isJumping = false;
			attacker.GetComponent<AttackerManager>().setAnimation ("isJumping", false);
			attacker.GetComponent<AttackerManager> ().arms [0].GetComponent<Animator> ().SetBool ("isJumping", false);
			attacker.GetComponent<AttackerManager> ().arms [1].GetComponent<Animator> ().SetBool ("isJumping", false);
			setOmbre (true);
        }
    }

    public void attackerCollideSol()
    {
        isJumping = false;
        defenderIsJumping = false;
        attacker.GetComponent<AttackerManager>().setAnimation("isJumping", false);
        attacker.GetComponent<AttackerManager>().arms[0].GetComponent<Animator>().SetBool("isJumping", false);
        attacker.GetComponent<AttackerManager>().arms[1].GetComponent<Animator>().SetBool("isJumping", false);
        setOmbre(true);
    }

	void OnPauseGame(){
		this.enabled = false;
	}

	void OnResumeGame(){
		this.enabled = true;
	}
}
