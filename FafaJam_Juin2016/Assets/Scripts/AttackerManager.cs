using UnityEngine;
using System.Collections;

public class AttackerManager : MonoBehaviour {
	
    private WeaponManager weaponManager;
    private PlayerManager playerManager;

	public Transform[] arms;

    void Start () {
        weaponManager = gameObject.GetComponent<WeaponManager>();
        playerManager = gameObject.transform.parent.gameObject.GetComponent<PlayerManager>();
		//arms = this.GetComponentsInChildren<Transform> ();
		print (arms.Length.ToString ());
    }

    void Update () {
        shoot();
		updateArms ();
    }

    public void shoot()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector3 mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
            Vector3 playerPosition = gameObject.transform.position;
            Vector3 finalPosition;
<<<<<<< HEAD
=======
            print(new Vector3(mousePosition.x - playerPosition.x, mousePosition.y - playerPosition.y, playerPosition.z));
>>>>>>> 84ed6fc99e74bf80534c765912db4531387dabe6
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
            }
				
            weaponManager.directionAttack = finalPosition;
            weaponManager.Attack(false);
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
			}


		arms[0].rotation = Quaternion.Euler (0f, 0f, (Mathf.Atan2 ((finalPosition.y), (finalPosition.x)) * Mathf.Rad2Deg));
		arms[1].rotation = Quaternion.Euler (0f, 0f, 2.5f*(Mathf.Atan2 ((finalPosition.y), (finalPosition.x)) * Mathf.Rad2Deg));
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
