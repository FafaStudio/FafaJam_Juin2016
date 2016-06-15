using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScoreDisplay : MonoBehaviour {
    public GameObject textDisplayer;
    ScoreManager scoreManager;
    HighScore highScore;
    // Use this for initialization
    void Start () {
        scoreManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreManager>();
        highScore = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HighScore>();
    }
    void Awake()
    {
        scoreManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreManager>();
        highScore = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HighScore>();
    }

    // Update is called once per frame
    void Update () {
	
	}
    public void displayScores()
    {
        GameObject actualTextDisplayer;
        print(scoreManager.scoreToString());
        foreach (string score in scoreManager.scoreToString())
        {
            actualTextDisplayer = Instantiate(textDisplayer);
            actualTextDisplayer.transform.SetParent(transform.Find("EnemyKilled"));
            actualTextDisplayer.GetComponent<Text>().text = score;
        }
        foreach (string score in highScore.highScoreToString())
        {
            actualTextDisplayer = Instantiate(textDisplayer);
            actualTextDisplayer.transform.SetParent(transform.Find("HighScore"));
            actualTextDisplayer.GetComponent<Text>().text = score;
        }
        foreach (string stat in scoreManager.statToString())
        {
            actualTextDisplayer = Instantiate(textDisplayer);
            actualTextDisplayer.transform.SetParent(transform.Find("RandomStats"));
            actualTextDisplayer.GetComponent<Text>().text = stat;
        }
    }

    public void unDisplayScores()
    {
        foreach (Transform child in transform.Find("EnemyKilled"))
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Transform child in transform.Find("HighScore"))
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Transform child in transform.Find("RandomStats"))
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
