using UnityEngine;
using System.Collections;

public class DefenderManager : MonoBehaviour {
    // Use this for initialization
    private WeaponManager weaponManager;
    private PlayerManager playerManager;


	public GameObject bubbleEffect;
	private float timeBetweenBuble;

	private Animator canonAnim;

    void Start()
    {
        weaponManager = gameObject.GetComponent<WeaponManager>();
        playerManager = this.transform.parent.gameObject.GetComponent<PlayerManager>();

		timeBetweenBuble = Random.Range (2f, 4f);
		canonAnim = GameObject.Find ("canon").GetComponent<Animator> ();
    }

    void Update()
    {
		launchBubleParticle ();
        grenade();
    }

    public void grenade()
    {
		Vector2 direction;
		if (Input.GetKey(KeyCode.A))
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreManager>().addStat("Grenades Lancées");
            if (playerManager.getSwapPosition())//tireur a gauche
            {
				direction = new Vector2 (20f, 10f);
            }
            else
            {
				direction = new Vector2 (-20f, 10f);
            }
			if (weaponManager.CanAttack) {
				canonAnim.SetTrigger ("Firing");
			}
			weaponManager.launchGrenade (direction);
        }
    }

	public void launchBubleParticle(){
		if (timeBetweenBuble > 0) {
			timeBetweenBuble -= Time.deltaTime;
		} else {
			if (playerManager.getSwapPosition ()) {//bernrd a droite
				Instantiate (bubbleEffect, new Vector3 (this.transform.position.x+0.3f , this.transform.position.y -0.2f), Quaternion.identity);
			}else

				Instantiate (bubbleEffect, new Vector3 (this.transform.position.x -0.3f, this.transform.position.y - 0.2f), Quaternion.identity);
			timeBetweenBuble = Random.Range (2f, 4f);
		}
	}
}
