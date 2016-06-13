﻿using UnityEngine;
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
		anim = this.GetComponent<Animator> ();
		weapon = this.GetComponent<WeaponManager> ();
		maxRate = weapon.shootingRate;
		movement = this.GetComponent<MovementScript> ();
		rangeToPlayer = Random.Range (4f, 7f);
		timeBetweenBuble = Random.Range (2f, 4f);
		this.GetComponent<BoxCollider2D> ().size = new Vector2(1.5f, Random.Range (3f, 3.6f));
	}

	void Update () {
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

	public override void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "TIRPlayer") {
			this.pv -= 1;
			Destroy (coll.gameObject);
			StartCoroutine (this.GetComponent<HitColorChange>().launchHit());
			if (this.pv <= 0) {
				//manager.updateScore (scoreValue);
				StartCoroutine(startDeath());
			}
		}
	}

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
		

	void OnPauseGame(){
		this.enabled = false;
	}

	void OnResumeGame(){
		this.enabled = true;
	}
}
