using UnityEngine;
using System.Collections;

public class DefenderManager : MonoBehaviour {
    // Use this for initialization
    private WeaponManager weaponManager;
    private PlayerManager playerManager;
    void Start()
    {
        weaponManager = gameObject.GetComponent<WeaponManager>();
        playerManager = gameObject.transform.parent.gameObject.GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        grenade();

    }

    public void grenade()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Vector3 mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
            Vector3 playerPosition = gameObject.transform.position;
            Vector3 finalPosition;
            if (playerManager.getSwapPosition())//tireur a gauche
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
            else
            {
                if (mousePosition.x - playerPosition.x >= 0)//si dans le dos
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


            weaponManager.directionGrenade = finalPosition;
            weaponManager.GrenadeLaunch(false);
        }
    }
}
