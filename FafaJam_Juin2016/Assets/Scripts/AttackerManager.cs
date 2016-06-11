using UnityEngine;
using System.Collections;

public class AttackerManager : MonoBehaviour {
    // Use this for initialization
    private WeaponManager weaponManager;
    private PlayerManager playerManager;
    void Start () {
        weaponManager = gameObject.GetComponent<WeaponManager>();
        playerManager = gameObject.transform.parent.gameObject.GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update () {
        shoot();

    }

    public void shoot()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector3 mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
            Vector3 playerPosition = gameObject.transform.position;
<<<<<<< HEAD
            Vector3 finalPosition;
            if (playerManager.getSwapPosition())//tireur a gauche
            {
                if(mousePosition.x - playerPosition.x >= playerPosition.x)//si dans le dos
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
                    finalPosition = new Vector3(playerPosition.x, shotPositionY, playerPosition.z);
                }
                else
                {
                    finalPosition = new Vector3(mousePosition.x - playerPosition.x, mousePosition.y - playerPosition.y, playerPosition.z);
                }
            }
            else
            {
                if (mousePosition.x - playerPosition.x <= playerPosition.x)//si dans le dos
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
                    finalPosition = new Vector3(playerPosition.x, shotPositionY, playerPosition.z);
                }
                else
                {
                    finalPosition = new Vector3(mousePosition.x - playerPosition.x, mousePosition.y - playerPosition.y, playerPosition.z);
                }
            }
=======
>>>>>>> 873f7f9f0ff7d3aa3abed9086ff327034eac0266


            weaponManager.directionAttack = finalPosition;
            weaponManager.Attack(false);
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
