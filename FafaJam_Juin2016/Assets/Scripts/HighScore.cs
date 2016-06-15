using UnityEngine;
using System.Collections;

public class HighScore : MonoBehaviour {

    // Use this for initialization


    string[] highScoreNames = new string[] { "HighScore0", "HighScore1", "HighScore2", "HighScore3", "HighScore4", "HighScore5", "HighScore6", "HighScore7", "HighScore8", "HighScore9" };
	void Start () {
        if (!PlayerPrefs.HasKey(highScoreNames[0])){
            createHighScore();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void createHighScore()
    {
        for (int i = 0; i < highScoreNames.Length; i++)
        {
            PlayerPrefs.SetInt(highScoreNames[i], 0);
        }
    }
    void addScore(int newScore)
    {
        for (int i = 0; i < highScoreNames.Length; i++)
        {
            if (PlayerPrefs.GetInt(highScoreNames[i]) < newScore)
            {
                addScore(PlayerPrefs.GetInt(highScoreNames[i]));
                PlayerPrefs.SetInt(highScoreNames[i], newScore);
                break;
            }
        }
            /*
            int lowerScore = 99999999;
            int lowerId = 0;
            for (int i = 0; i < highScoreNames.Length; i++)
            {
                if (PlayerPrefs.GetInt(highScoreNames[i]) < lowerScore)
                {
                    lowerScore = PlayerPrefs.GetInt(highScoreNames[i]);
                    lowerId = i;
                }
            }
            if (PlayerPrefs.GetInt(highScoreNames[lowerId]) < newScore)
            {
                PlayerPrefs.SetInt(highScoreNames[lowerId], newScore);
            }*/
    }
    public string[] highScoreToString()
    {
        string[] highScoreStringed = new string[highScoreNames.Length];
        for (int i = 0; i < highScoreNames.Length; i++)
        {
            print(highScoreNames[i] + " : " + PlayerPrefs.GetInt(highScoreNames[i]));
            highScoreStringed[i] = highScoreNames[i] + " : " + PlayerPrefs.GetInt(highScoreNames[i]);
        }
        return highScoreStringed;
    }

}
