using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	public Transform explosionGameObject;

	public void launchExplosion(GameObject parent){
		var explosionTransform = Instantiate(explosionGameObject) as Transform;
		explosionTransform.parent = parent.transform;
		explosionTransform.position = this.transform.position;
	}
}
