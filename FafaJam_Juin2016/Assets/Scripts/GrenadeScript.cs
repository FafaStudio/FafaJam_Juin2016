using UnityEngine;
using System.Collections;

public class GrenadeScript : ShotScript {

	private float cptVelocity = 5f;

	public override void Start()
	{
		base.Start();
		Destroy (this.gameObject, 3f);
	}

	void Update(){
		if (cptVelocity > 0) {
			cptVelocity--;
		} else
			this.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);
		if (!isTouching)
			return;
		Destroy (this.gameObject);
	}
}
