using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	public Transform explosionGameObject;

	public void launchExplosion(GameObject parent, bool isRandomPosition){
		var explosionTransform = Instantiate(explosionGameObject) as Transform;
		explosionTransform.parent = parent.transform;
		if (!isRandomPosition) {
			explosionTransform.position = this.transform.position;
		}else
			explosionTransform.position = new Vector2(this.transform.position.x + Random.Range(-4f, 4f), this.transform.position.y + Random.Range(-4f, 4f));
		explosionTransform.localScale = new Vector3(1, 1, 0);
	}
}
