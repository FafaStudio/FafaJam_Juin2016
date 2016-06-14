using UnityEngine;
using System.Collections;

public class BasicEnemy : EnemyScript {

	private WeaponManager weapon;
	private float maxRate;

	public MovementScript movement;

	private Animator anim;

	public bool isMoving = false;

	private bool isSpawnedRight;

	private float rangeToPlayer;

	public bool cantShoot = false;

	public GameObject bubbleEffect;
	private float timeBetweenBuble;

	void Start () {
        enemyName = "BasicEnemy";
		anim = this.GetComponent<Animator> ();
		weapon = this.GetComponent<WeaponManager> ();
		maxRate = weapon.shootingRate;
		movement = this.GetComponent<MovementScript> ();
		rangeToPlayer = Random.Range (4f, 7f);
		timeBetweenBuble = Random.Range (2f, 4f);
		this.GetComponent<BoxCollider2D> ().size = new Vector2(1.5f, Random.Range (3f, 3.6f));
	}

	void Update () {
		if ((isDead)||(target == null))
			return;
		launchBubleParticle ();
		if (isSpawnedRight) {
			if ((this.transform.position.x - target.transform.position.x) < rangeToPlayer) {
				cantShoot = false;
				movement.isStopped = false;
				movement.setDirection (1, 0);
				anim.SetBool ("isFront", false);
			} else if ((this.transform.position.x - target.transform.position.x) > rangeToPlayer + 1) {
				cantShoot = true;
				movement.isStopped = false;
				movement.setDirection (-1, 0);
				anim.SetBool ("isFront", true);
			} else {
				cantShoot = false;
				movement.isStopped = true;
			}
		} else {
			if ((target.transform.position.x - this.transform.position.x) < rangeToPlayer) {
				cantShoot = false;
				movement.isStopped = false;
				movement.setDirection (-1, 0);
				this.transform.localScale = new Vector2 (-0.7f, 0.7f);
				anim.SetBool ("isFront", false);
			} else if ((target.transform.position.x - this.transform.position.x) > rangeToPlayer + 1) {
				cantShoot = true;
				movement.isStopped = false;
				movement.setDirection (1, 0);
				this.transform.localScale = new Vector2 (-0.7f, 0.7f);
				anim.SetBool ("isFront", true);
			} else {
				cantShoot = false;
				movement.isStopped = true;
			}
		}

		weapon.shootingRate -= Time.deltaTime;
		if ((weapon.shootingRate <= 0)||(!cantShoot)) {
			setDirectionAttack ();
			doAttack ();
		}
	}

	public void setSideSpawned(bool value){
		isSpawnedRight = value;
	}

	private void doAttack(){
		if(weapon!=null){
			if (weapon.CanAttack) {
				anim.SetTrigger ("Firing");
			}
			weapon.Attack (true);
			weapon.shootingRate = maxRate;
		}
	}

	public void setDirectionAttack(){
		if (!isSpawnedRight) {
			weapon.directionAttack = new Vector2(1f, 0f);
		}else
			weapon.directionAttack = new Vector2(-1f, 0f);
	}
    /*
	public override void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "TIRPlayer") {
			this.pv -= 1;
			Destroy (coll.gameObject);
			StartCoroutine (this.GetComponent<HitColorChange>().launchHit());
			if (this.pv <= 0) {
				launchExplosion (this.gameObject);
				StartCoroutine(startDeath());
			}
		}
	}*/

	public void launchBubleParticle(){
		if (timeBetweenBuble > 0) {
			timeBetweenBuble -= Time.deltaTime;
		} else {
			if (isSpawnedRight) {//tireur a gauche
				Instantiate (bubbleEffect, new Vector3 (this.transform.position.x - 0.3f, this.transform.position.y + 1f), Quaternion.identity);
			}else
				Instantiate (bubbleEffect, new Vector3 (this.transform.position.x + 0.3f, this.transform.position.y + 1f), Quaternion.identity);
			timeBetweenBuble = Random.Range (2f, 4f);
		}
	}

	/*public override IEnumerator startDeath(){
		GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreManager>().addScore(enemyName);
		this.GetComponent<SpriteRenderer> ().sprite = null;
		isDead = true;
		this.enabled = false;
		//launchExplosion (this.gameObject);
		//yield return new WaitForSeconds (0.2f);
		var puTransform = Instantiate (fumeParticle) as Transform;
		puTransform.position = this.transform.position;
		yield return new WaitForSeconds (0.5f);
		Destroy (this.gameObject);
	}*/

	public void launchExplosion(GameObject parent){
		var explosionTransform = Instantiate(this.GetComponent<Explosion>().explosionGameObject, this.transform.position, Quaternion.identity) as Transform;
		explosionTransform.parent = parent.transform;
		//explosionTransform.position = this.transform.position;
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "TIRPlayer")
        {
            this.pv -= 1;
            Destroy(coll.gameObject);
            StartCoroutine(this.GetComponent<HitColorChange>().launchHit());

            if (this.pv == 0)
            {
                StartCoroutine(startDeath());
            }
        }
        else if(coll.gameObject.tag == "Player")
        {
            this.GetComponent<BoxCollider2D>().isTrigger = true;
            this.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }

    public void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            this.GetComponent<BoxCollider2D>().isTrigger = false;
            this.GetComponent<Rigidbody2D>().gravityScale = 10;
        }
    }



    void OnPauseGame(){
		this.enabled = false;
	}

	void OnResumeGame(){
		this.enabled = true;
	}
}
