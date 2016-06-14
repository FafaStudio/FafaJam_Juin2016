using UnityEngine;
using System.Collections;

public class Blobfish : EnemyScript {


	public float rotation = 100f;

	void Start () {
        enemyName = "Blobfish";
        print(target.transform.position);
        print(this.transform.position);
        
        this.GetComponent<MovementScript>().direction = new Vector2(target.transform.position.x - this.transform.position.x, target.transform.position.y - this.transform.position.y) * Mathf.Abs((1/(target.transform.position.x - this.transform.position.x)));
    }

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
