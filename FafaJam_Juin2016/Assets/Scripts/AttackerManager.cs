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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //WeaponManager.att            
            //Instantiate(particle, transform.position, transform.rotation);
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
