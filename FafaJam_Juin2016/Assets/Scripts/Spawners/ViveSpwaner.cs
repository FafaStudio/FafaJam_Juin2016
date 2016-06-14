using UnityEngine;
using System.Collections;

public class ViveSpwaner : SpawnerManager {

	public override void spawnBasicEnnemy(){
		if (chronoBasicEnemy <= 0f) {
			if (spawnerBox.GetComponentsInChildren<Transform> ().Length > 5) {
				setSequence (0, 0);
			}
			var puTransform = Instantiate(enemyBasic) as Transform;
			Vector3 newPos = new Vector3 (this.transform.position.x, this.transform.position.y, 0f);

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
