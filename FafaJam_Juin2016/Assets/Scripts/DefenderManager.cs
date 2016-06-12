using UnityEngine;
using System.Collections;

public class DefenderManager : MonoBehaviour {
    // Use this for initialization
    private WeaponManager weaponManager;
    private PlayerManager playerManager;

    void Start()
    {
        weaponManager = gameObject.GetComponent<WeaponManager>();
        playerManager = this.transform.parent.gameObject.GetComponent<PlayerManager>();
    }

    void Update()
    {
        grenade();
    }

    public void grenade()
    {
		Vector2 direction;
		if (Input.GetKey(KeyCode.A))
        {
            if (playerManager.getSwapPosition())//tireur a gauche
            {
				direction = new Vector2 (20f, 10f);
            }
            else
            {
				direction = new Vector2 (-20f, 10f);
            }
				
			weaponManager.launchGrenade (direction);
        }
    }
}
