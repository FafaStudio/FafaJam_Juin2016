using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {


    public string itemName;
    public bool bonus;
    SpriteRenderer spriteRenderer;
    public PlayerManager playerManager;
    private Rigidbody2D body;


    // Use this for initialization
    public virtual void Awake() {
        playerManager = GameObject.FindWithTag("Player").GetComponent<PlayerManager>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        body = this.GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update () {
	
	}

    public virtual void itemEffect()
    {

    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            itemEffect();
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Platform")
        {
            gameObject.transform.parent = col.gameObject.transform;
            body.isKinematic = true;
        }
    }

    public void changeColor(Color color)
    {
        spriteRenderer.color = color;
    }
}
