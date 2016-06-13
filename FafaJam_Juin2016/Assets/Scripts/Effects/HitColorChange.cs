using UnityEngine;
using System.Collections;

public class HitColorChange : MonoBehaviour {

	public Color hitColor;
	public Color normalColor;

	public IEnumerator launchHit(){
		this.GetComponent<SpriteRenderer> ().color = hitColor;
		yield return new WaitForSeconds (0.05f);
		this.GetComponent<SpriteRenderer> ().color = normalColor;
	}
}
