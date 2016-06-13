using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScoreDisplay : MonoBehaviour {
    public GameObject textDisplayer;
    ScoreManager scoreManager;
	// Use this for initialization
	void Start () {
        scoreManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreManager>();
        displayScores();
    }

    // Update is called once per frame
    void Update () {
	
	}
    public void displayScores()
    {
        GameObject actualTextDisplayer;
        foreach (string score in scoreManager.scoreToString())
        {
            actualTextDisplayer = Instantiate(textDisplayer);
            actualTextDisplayer.transform.SetParent(transform);
            actualTextDisplayer.GetComponent<Text>().text = score;
        }
        foreach (string stat in scoreManager.statToString())
        {
            actualTextDisplayer = Instantiate(textDisplayer);
            actualTextDisplayer.transform.SetParent(transform);
            actualTextDisplayer.GetComponent<Text>().text = stat;
        }
    }
}
