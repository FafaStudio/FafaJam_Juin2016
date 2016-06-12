using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {


    public string itemName;
    public bool bonus;
    SpriteRenderer itemSprite;
    public PlayerManager playerManager;

	// Use this for initialization
	public virtual void Start () {
        playerManager = GameObject.FindWithTag("Player").GetComponent<PlayerManager>();
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
}
