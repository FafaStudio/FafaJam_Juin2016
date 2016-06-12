using UnityEngine;
using System.Collections;

public class GrenadeScript : ShotScript {

    public float flyingTime;

	public override void Start()
	{
		base.Start();
		Destroy (this.gameObject, 3f);
        flyingTime = 2f;
    }

	void Update(){
        if(flyingTime > 0)
        {
            flyingTime -= Time.deltaTime;
        }
        else
			this.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);
		if (!isTouching)
			return;
		Destroy (this.gameObject);
	}
}
