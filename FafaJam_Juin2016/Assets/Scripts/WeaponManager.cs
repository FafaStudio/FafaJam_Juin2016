using UnityEngine;


public class WeaponManager : MonoBehaviour
{

	public Transform shotPrefab;

	public float shootingRate = 0.25f;

	private float shootCooldown;

	public Vector2 directionAttack;

	void Start()
	{
		shootCooldown = 0f;
	}

	void Update()
	{
		if (shootCooldown > 0)
		{
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
			ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
			/*if (shot != null)
			{
				shot.isEnemyShot = isEnemy;
				if (!shot.isEnemyShot) {
					Vector3 bullPosition = new Vector3 (transform.position.x + 10f, transform.position.y+3f, 0f);
					shotTransform.position = bullPosition;
				}
			}*/
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