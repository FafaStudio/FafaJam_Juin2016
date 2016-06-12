using UnityEngine;
using System.Collections;

public class GrenadeScript : ShotScript {

    public float flyingTime;
    private CircleCollider2D circleCollider2D;

	public override void Start()
	{
		base.Start();
		Destroy (this.gameObject, 3f);
        flyingTime = 2f;
        circleCollider2D = gameObject.GetComponent<CircleCollider2D>();
        circleCollider2D.enabled = false;
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

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (circleCollider2D.enabled == false && (col.gameObject.tag == "Sol" || col.gameObject.tag == "Enemy"))
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            circleCollider2D.enabled = true;
            Destroy(gameObject, 0.1f);
        }
        else if(circleCollider2D == true && col.gameObject.tag == "Enemy")
        {
            Destroy(col.gameObject);
        }
    }

    public void onColliderEnter2D(Collider2D col)
    {
        if (circleCollider2D == true && col.gameObject.tag == "Enemy")
        {
            Destroy(col.gameObject);
        }
    }
}
