﻿using UnityEngine;


public class WeaponManager : MonoBehaviour
{

	public Transform shotPrefab;

	public float shootingRate = 0.25f;

	private float shootCooldown;

    public Vector2 directionAttack;

	public Quaternion rotationShoot;

	void Start()
	{
		shootCooldown = 0f;
	}

	void Update()
	{
		if (shootCooldown > 0) {
			shootCooldown -= Time.deltaTime;
		}
	}

	public void Attack(bool isEnemy)
	{
		if (CanAttack)
		{
			shootCooldown = shootingRate;
			var shotTransform = Instantiate(shotPrefab) as Transform;
			shotTransform.position = transform.position;
			shotTransform.gameObject.GetComponent<MovementScript> ().direction = directionAttack;
		}
	}	

	public void AttackWithSpecialPosition(Vector3 bullPosition)
	{
		if (CanAttack)
		{
			shootCooldown = shootingRate;
			var shotTransform = Instantiate(shotPrefab) as Transform;
			shotTransform.position = bullPosition;
			shotTransform.gameObject.GetComponent<MovementScript> ().direction = directionAttack;
			shotTransform.rotation = rotationShoot;
		}
	}	

	public void launchGrenade(Vector2 direction){
		if (CanAttack)
		{
			shootCooldown = shootingRate;
			var shotTransform = Instantiate(shotPrefab) as Transform;
			shotTransform.position = this.transform.position;
			//shotTransform.gameObject.GetComponent<MovementScript> ().direction = directionAttack;
			shotTransform.GetComponent<Rigidbody2D>().velocity = direction;
		}
	}

	public bool CanAttack
	{
		get
		{
			return shootCooldown <= 0f;
		}
	}
}