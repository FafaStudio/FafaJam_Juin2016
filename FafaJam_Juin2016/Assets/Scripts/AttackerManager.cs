using UnityEngine;
using System.Collections;

public class AttackerManager : MonoBehaviour {
    // Use this for initialization
    private WeaponManager weaponManager;
    void Start () {
        weaponManager = gameObject.GetComponent<WeaponManager>();
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

            weaponManager.directionAttack = new Vector3(mousePosition.x - playerPosition.x, mousePosition.y - playerPosition.y, playerPosition.z);
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
        /*

    mouse_pos = Input.mousePosition;
     mouse_pos.z = 5.23; //The distance between the camera and object
     object_pos = Camera.main.WorldToScreenPoint(target.position);
     mouse_pos.x = mouse_pos.x - object_pos.x;
     mouse_pos.y = mouse_pos.y - object_pos.y;
     angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
     transform.rotation = Quaternion.Euler(Vector3(0, 0, angle));*/
}
