using UnityEngine;
using System.Collections;

public class Blobfish : EnemyScript {


	public float rotation = 100f;

	private AudioSource soundDisplayer;

	void Start () {
		soundDisplayer = this.GetComponent<AudioSource> ();
        enemyName = "Blobfish";  
		if (target == null)
			return;
        this.GetComponent<MovementScript>().direction = new Vector2(target.transform.position.x - this.transform.position.x, target.transform.position.y - this.transform.position.y) * Mathf.Abs((1/(target.transform.position.x - this.transform.position.x)));
    }

    void Update () {
		if ((isDead)||(target == null))
			return;
		transform.Rotate (Vector3.back * (rotation * Time.deltaTime));
	}

	public override void OnTriggerEnter2D (Collider2D coll)
	{
		base.OnTriggerEnter2D (coll);
		if (coll.gameObject.tag == "Sol") {
			this.GetComponent<MovementScript> ().direction = new Vector2 (this.GetComponent<MovementScript> ().direction.x, -this.GetComponent<MovementScript> ().direction.y);
			soundDisplayer.Play ();
		}
		if (coll.gameObject.tag == "Player") {
			soundDisplayer.Play ();
			Destroy (this.gameObject);
		}
		if (coll.gameObject.tag == "Bouclier") {
			soundDisplayer.Play ();
			this.GetComponent<MovementScript> ().direction = new Vector2 (this.GetComponent<MovementScript> ().direction.x, -this.GetComponent<MovementScript> ().direction.y);
		}
	}

	private Vector3 miseAuPointShoot(Vector3 supposedPosition)
	{
		return supposedPosition *0.75f / Mathf.Abs(supposedPosition.x);
	}

}
