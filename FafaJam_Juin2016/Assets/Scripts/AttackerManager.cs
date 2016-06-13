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



    private bool needRecadrageShoot(bool swapPosition, Vector3 supposedPosition)
    {//si on est à l'opposé de notre rayon de tir ou trop bas
        return ((swapPosition && supposedPosition.x > 0) || (!swapPosition && supposedPosition.x < 0) || ((supposedPosition.x == 0 && supposedPosition.y < -1) || supposedPosition.y / Mathf.Abs(supposedPosition.x)<-0.5f));
    }


    private Vector3 recadrageShoot(bool swapPosition, Vector3 supposedPosition)
    {
        Vector3 recadredShoot;
        if(supposedPosition.y < 0)
        {
            if (swapPosition)
            {
                recadredShoot = new Vector3(-6f, -3f, 0);
            }
            else
            {
                recadredShoot = new Vector3(6f, -3f, 0);
            }
        }
        else
        {
            recadredShoot = new Vector3(0, 4f, 0);
        }
        return recadredShoot;
    }

    private Vector3 miseAuPointShoot(bool swapPosition, Vector3 supposedPosition)
    {
        return supposedPosition * 3 / Mathf.Abs(supposedPosition.x);
    }

    public void shoot()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreManager>().addStat("Tirs Effectués");
            Vector3 mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
            Vector3 playerPosition = gameObject.transform.position;
            Vector3 calculatedPosition = new Vector3(mousePosition.x - playerPosition.x, mousePosition.y - playerPosition.y, playerPosition.z);
            Vector3 finalPosition;
            if(needRecadrageShoot(playerManager.getSwapPosition(), calculatedPosition)){
                finalPosition = recadrageShoot(playerManager.getSwapPosition(), calculatedPosition);
            }
            else
            {
                finalPosition = miseAuPointShoot(playerManager.getSwapPosition(), calculatedPosition);
            }

			arms [0].GetComponent<Animator> ().SetTrigger ("Firing");
			arms [1].GetComponent<Animator> ().SetTrigger ("Firing");
			playerManager.camera.shakeAmount = 0.05f;
			playerManager.camera.setShake (0.01f);
            weaponManager.directionAttack = finalPosition;
			weaponManager.AttackWithSpecialPosition (GameObject.Find ("WeaponCanonMorue").transform.position);
		
        }
    }

    public void updateArms()
	{
            Vector3 mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
            Vector3 playerPosition = gameObject.transform.position;
            Vector3 calculatedPosition = new Vector3(mousePosition.x - playerPosition.x, mousePosition.y - playerPosition.y, playerPosition.z);
            Vector3 finalPosition;
            if (needRecadrageShoot(playerManager.getSwapPosition(), calculatedPosition))
            {
                finalPosition = recadrageShoot(playerManager.getSwapPosition(), calculatedPosition);
            }
            else
            {
                finalPosition = miseAuPointShoot(playerManager.getSwapPosition(), calculatedPosition);
            }
        if (playerManager.getSwapPosition())
        {
            arms[0].rotation = Quaternion.Euler (0f, 0f, 180-(Mathf.Atan2 ((finalPosition.y), (finalPosition.x)) * Mathf.Rad2Deg));
			arms[1].rotation = Quaternion.Euler (0f, 0f, 30+(-2f*(Mathf.Atan2 ((finalPosition.y), (finalPosition.x)) * Mathf.Rad2Deg)));
        }
        else
        {
            arms[0].rotation = Quaternion.Euler(0f, 0f, (Mathf.Atan2((finalPosition.y), (finalPosition.x)) * Mathf.Rad2Deg));
            arms[1].rotation = Quaternion.Euler(0f, 0f, 2.5f * (Mathf.Atan2((finalPosition.y), (finalPosition.x)) * Mathf.Rad2Deg));
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

	public void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "enemyBullet") {
			//playerManager.takeDamage(1);
			Destroy (coll.gameObject);
			playerManager.camera.setShake (0.05f);
			StartCoroutine (this.GetComponent<HitColorChange>().launchHit());
			foreach (HitColorChange hit in GetComponentsInChildren<HitColorChange>()) {
				StartCoroutine (hit.launchHit());
			}
			if (playerManager.getPv() <= 0) {
				//manager.updateScore (scoreValue);
				Destroy (this.gameObject);
			}
		}
	}

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Sol")
        {
            playerManager.attackerCollideSol();
        }
    }

	void OnPauseGame(){
		this.enabled = false;
	}

	void OnResumeGame(){
		this.enabled = true;
	}

}
