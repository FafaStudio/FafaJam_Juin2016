using UnityEngine;
using System.Collections;

public class BaleineMissile : MonoBehaviour {

	public Transform fumeParticle;

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Player") {
			StartCoroutine (startDeath ());
		}
		if (coll.gameObject.tag == "Sol") {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreManager>().addStat("Missiles Esquivés");
            StartCoroutine(startDeath ());
		}
		if (coll.gameObject.tag == "Bouclier") {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreManager>().addStat("Missiles Détruits");
            StartCoroutine(startDeath ());
		}
	}
    /*
	public IEnumerator startDeath(){
		var puTransform = Instantiate (fumeParticle) as Transform;
		puTransform.position = this.transform.position;
		yield return new WaitForSeconds (0.1f);
		Destroy (this.gameObject);
	}*/

    public IEnumerator startDeath()
    {
        this.GetComponent<SpriteRenderer>().sprite = null;
        this.GetComponent<Explosion>().launchExplosion(this.gameObject);
        yield return new WaitForSeconds(0.2f);
        var puTransform = Instantiate(fumeParticle) as Transform;
        puTransform.position = this.transform.position;
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }


}
