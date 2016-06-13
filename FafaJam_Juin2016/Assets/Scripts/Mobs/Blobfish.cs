using UnityEngine;
using System.Collections;

public class Blobfish : EnemyScript {


	public float rotation = 100f;

	void Start () {
		if (target.transform.position.x < 0) {
			this.GetComponent<MovementScript> ().direction = new Vector2 (this.transform.position.x - target.transform.position.x, this.transform.position.y - target.transform.position.y + 12);
		}else
			this.GetComponent<MovementScript> ().direction = new Vector2 (this.transform.position.x - target.transform.position.x, this.transform.position.y - target.transform.position.y );
		this.GetComponent<MovementScript> ().speed = miseAuPointShoot((target.transform.position - this.transform.position).normalized*0.75f );
		Destroy (this, 6f);
	}

	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.back * (rotation * Time.deltaTime));
	}

	public override void OnTriggerEnter2D (Collider2D coll)
	{
		base.OnTriggerEnter2D (coll);
		if (coll.gameObject.tag == "Sol") {
			this.GetComponent<MovementScript> ().direction = new Vector2 (this.GetComponent<MovementScript> ().direction.x, -this.GetComponent<MovementScript> ().direction.y);
		}
		if (coll.gameObject.tag == "Player") {
			Destroy (this.gameObject);
		}
	}

	private Vector3 miseAuPointShoot(Vector3 supposedPosition)
	{
		return supposedPosition *0.75f / Mathf.Abs(supposedPosition.x);
	}

}
