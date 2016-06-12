using UnityEngine;


public class WeaponManager : MonoBehaviour
{

	public Transform shotPrefab;

	public float shootingRate = 0.25f;

	private float shootCooldown;

    public Vector2 directionAttack;

    public Transform grenadePrefab;

    public float grenadeRate = 3f;

    private float grenadeCooldown;

    public Vector2 directionGrenade;


	void Start()
	{
		shootCooldown = 0f;
        grenadeCooldown = 0f;
	}

	void Update()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
        if (grenadeCooldown > 0)
        {
            grenadeCooldown -= Time.deltaTime;
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
			if (shot != null)
			{
				shot.isEnemyShot = isEnemy;
				if (!shot.isEnemyShot) {
					
					Vector3 bullPosition = GameObject.Find("WeaponCanon").transform.position;
					shotTransform.position = bullPosition;
				}
			}
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
			ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
		}
	}	

	public bool CanAttack
	{
		get
		{
			return shootCooldown <= 0f;
		}
	}

    public void GrenadeLaunch(bool isEnemy) {
        if (CanLauchGrenade)
        {
            grenadeCooldown = grenadeRate;
            var grenadeTransform = Instantiate(grenadePrefab) as Transform;
            grenadeTransform.position = transform.position;
            grenadeTransform.gameObject.GetComponent<GrenadeMovementScript>().direction = directionGrenade;
            GrenadeScript shot = grenadeTransform.gameObject.GetComponent<GrenadeScript>();
        }
    }

    public bool CanLauchGrenade //demander à Hugo si c'est une structure Unity ou c# en générale
    {
        get
        {
            return grenadeCooldown <= 0f;
        }
    }
}