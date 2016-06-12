using UnityEngine;
using System.Collections;

public class AttackerManager : MonoBehaviour {
	
    private WeaponManager weaponManager;
    private PlayerManager playerManager;

	public Transform[] arms;

	private Animator animManager;

	public GameObject bubbleEffect;
	private float timeBetweenBuble;


    void Start () {
        weaponManager = gameObject.GetComponent<WeaponManager>();
        playerManager = gameObject.transform.parent.gameObject.GetComponent<PlayerManager>();
		//arms = this.GetComponentsInChildren<Transform> ();
		arms[0].GetComponent<Animator> ().SetBool ("isRight", true);
		animManager = this.GetComponent<Animator> ();
		animManager.SetBool ("isFront", true);

		timeBetweenBuble = Random.Range (2f, 4f);
    }

    void Update () {
        shoot();
		updateArms ();
		launchBubleParticle ();
    }

	public void setAnimation(string variable, bool value){
		animManager.SetBool (variable, value);
	}

    public void shoot()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector3 mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
            Vector3 playerPosition = gameObject.transform.position;
            Vector3 finalPosition;
            if (playerManager.getSwapPosition())//tireur a gauche
            {
                if(mousePosition.x - playerPosition.x >= 0)//si dans le dos
                {
                    float shotPositionY;
                    if(mousePosition.y - playerPosition.y >= 0)
                    {
                        shotPositionY = 4;
                    }
                    else
                    {
                        shotPositionY = -4;
                    }
                    finalPosition = new Vector3(0, shotPositionY, playerPosition.z);
                    
                }
                else
                {
                    float distanceX = mousePosition.x - playerPosition.x;
                    if (distanceX <= 3)
                    {
                        float multiplicateur = 3/Mathf.Abs(distanceX);
                        finalPosition = new Vector3(distanceX* multiplicateur, (mousePosition.y - playerPosition.y) * multiplicateur, playerPosition.z);
                    }
                    else
                    {
                        finalPosition = new Vector3(mousePosition.x - playerPosition.x, mousePosition.y - playerPosition.y, playerPosition.z);
                    }
                }
            }
            else
            {
                if (mousePosition.x - playerPosition.x <= 0)//si dans le dos
                {
                    float shotPositionY;
                    if (mousePosition.y - playerPosition.y >= 0)
                    {
                        shotPositionY = 4;
                    }
                    else
                    {
                        shotPositionY = -4;
                    }
                    finalPosition = new Vector3(0, shotPositionY, playerPosition.z);
                }
                else
                {
                    float distanceX = mousePosition.x - playerPosition.x;
                    if (distanceX <= 3)
                    {
                        float multiplicateur = 3 / Mathf.Abs(distanceX);
                        finalPosition = new Vector3(distanceX * multiplicateur, (mousePosition.y - playerPosition.y) * multiplicateur, playerPosition.z);
                    }
                    else
                    {
                        finalPosition = new Vector3(mousePosition.x - playerPosition.x, mousePosition.y - playerPosition.y, playerPosition.z);
                    }
                }
            }
			
			playerManager.camera.setShake (0.02f);
            weaponManager.directionAttack = finalPosition;
			weaponManager.AttackWithSpecialPosition (GameObject.Find ("WeaponCanonMorue").transform.position);
		
        }
    }

	public void updateArms()
	{

			Vector3 mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
			Vector3 playerPosition = gameObject.transform.position;
			Vector3 finalPosition;
			if (playerManager.getSwapPosition())//tireur a gauche
			{
				if(mousePosition.x - playerPosition.x >= 0)//si dans le dos
				{
					float shotPositionY;
					if(mousePosition.y - playerPosition.y >= 0)
					{
						shotPositionY = 4;
					}
					else
					{
						shotPositionY = -4;
					}
					finalPosition = new Vector3(0, shotPositionY, playerPosition.z);
				}
				else
				{
					finalPosition = new Vector3(mousePosition.x - playerPosition.x, mousePosition.y - playerPosition.y, playerPosition.z);
				}
				arms[0].rotation = Quaternion.Euler (0f, 0f, 180-(Mathf.Atan2 ((finalPosition.y), (finalPosition.x)) * Mathf.Rad2Deg));
			arms[1].rotation = Quaternion.Euler (0f, 0f, 30+(-2f*(Mathf.Atan2 ((finalPosition.y), (finalPosition.x)) * Mathf.Rad2Deg)));

			}
			else
			{
				if (mousePosition.x - playerPosition.x <= 0)//si dans le dos
				{
					float shotPositionY;
					if (mousePosition.y - playerPosition.y >= 0)
					{
						shotPositionY = 4;
					}
					else
					{
						shotPositionY = -4;
					}
					finalPosition = new Vector3(0, shotPositionY, playerPosition.z);
				}
				else
				{
					finalPosition = new Vector3(mousePosition.x - playerPosition.x, mousePosition.y - playerPosition.y, playerPosition.z);
				}
			arms[0].rotation = Quaternion.Euler (0f, 0f, (Mathf.Atan2 ((finalPosition.y), (finalPosition.x)) * Mathf.Rad2Deg));
			arms[1].rotation = Quaternion.Euler (0f, 0f, 2.5f*(Mathf.Atan2 ((finalPosition.y), (finalPosition.x)) * Mathf.Rad2Deg));
			}
		weaponManager.rotationShoot =  Quaternion.Euler (0f, 0f, (Mathf.Atan2 ((finalPosition.y), (finalPosition.x)) * Mathf.Rad2Deg));
	}

	public void launchBubleParticle(){
		if (timeBetweenBuble > 0) {
			timeBetweenBuble -= Time.deltaTime;
		} else {
			if (playerManager.getSwapPosition ()) {//tireur a gauche
				Instantiate (bubbleEffect, new Vector3 (this.transform.position.x - 0.3f, this.transform.position.y + 1f), Quaternion.identity);
			}else

				Instantiate (bubbleEffect, new Vector3 (this.transform.position.x + 0.3f, this.transform.position.y + 1f), Quaternion.identity);
			timeBetweenBuble = Random.Range (2f, 4f);
		}
	}

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
