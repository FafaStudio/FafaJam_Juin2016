using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeDisplay : MonoBehaviour {
    public Text text;
    public PlayerManager playerManager;
	// Use this for initialization
	void Start () {
        text = gameObject.GetComponent<Text>();
	    playerManager = GameObject.FindWithTag("Player").GetComponentInParent<PlayerManager>();
    }

    // Update is called once per frame
    void Update () {
        text.text = "PV : " + playerManager.getPv();
	}
}
