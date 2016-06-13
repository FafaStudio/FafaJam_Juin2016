using UnityEngine;
using System.Collections;

public class BaleineSpawner : SpawnerManager {


	public override void spawnBasicEnnemy(){
		if (chronoBasicEnemy <= 0f) {
			var puTransform = Instantiate(enemyBasic) as Transform;
			Vector3 newPos = new Vector3 (this.transform.position.x+Random.Range(-0.2f, 0.1f), this.transform.position.y, 0f);

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
