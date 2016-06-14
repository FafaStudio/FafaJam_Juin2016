using UnityEngine;
using System.Collections;

public class RaieSpawner : SpawnerManager {

	public override void spawnBasicEnnemy(){
		if (chronoBasicEnemy <= 0f) {
			if (spawnerBox.GetComponentsInChildren<Transform> ().Length > 5) {
				setSequence (0, 0);
			}
			var puTransform = Instantiate(enemyBasic) as Transform;
			Vector3 newPos = new Vector3 (this.transform.position.x, this.transform.position.y, 0f);
			if (isLeftSpawner) {
			//	puTransform.gameObject.GetComponent<BasicEnemy> ().setSideSpawned (false);
				puTransform.GetComponent<Raie> ().setDirection (1);
			} else {
			//	puTransform.gameObject.GetComponent<BasicEnemy> ().setSideSpawned (true);
				puTransform.GetComponent<Raie> ().setDirection (-1);
			}

			newPos = new Vector3 (this.transform.position.x, this.transform.position.y+Random.Range(-1f, 3.5f), 0f);
			//}

			puTransform.position = newPos;
			puTransform.SetParent (spawnerBox.transform);

			chronoBasicEnemy = setterChronoEnemy;
			cpt--;
			if (cpt <= 0) {
				isActiveMob = false;
			}

		}
	}
}
