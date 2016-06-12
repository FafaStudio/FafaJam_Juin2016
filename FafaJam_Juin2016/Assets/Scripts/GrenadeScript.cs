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

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Sol" || col.gameObject.tag == "Enemy")
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2f);
            for (int i = 0; i < colliders.Length; i++)
            {
                if(colliders[i].tag == "Enemy")
                {
                    Destroy(colliders[i].gameObject);
                }
            }
            Destroy(gameObject);
        }
    }
    /*private void OnDrawGizmos()//VOIR LA RANGE DU CERCLE
    {
        Gizmos.color = Color.red;
     //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
            Gizmos.DrawWireSphere(transform.position, 2f);
    }*/
}
