using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
	
    class ScoreElement
    {
        float multiplier;
        int value;

        public ScoreElement(float multiplier)
        {
            this.multiplier = multiplier;
            this.value = 0;
        }

        public float getScore()
        {
            return this.multiplier * this.value;
        }
        public void incremente()
        {
            this.value++;
        }
    }

    class RandomStat
    {
        float value;
        public RandomStat()
        {
            this.value = 0;
        }
        public float getStat()
        {
            return this.value;
        }
        public void incremente()
        {
            this.value++;
        }
        public void add(float added)
        {
            this.value += added;
        }
    }




    Dictionary<string, ScoreElement> scoreList;
    Dictionary<string, RandomStat> randomStatList;
    public float score;

	public Text scoreDisplayer;

    // Use this for initialization
    void Start () {
		scoreDisplayer = GameObject.Find ("ScoreDisplay").GetComponent<Text> ();
        if (scoreList == null)
        {
            setUp();
        }
    }

    void Update()
    {
        randomStatList["Timer"].add(Time.deltaTime);
    }

    private void setUp()
    {
        scoreList = new Dictionary<string, ScoreElement>();
        scoreList.Add("BasicEnemy", new ScoreElement(150f));
        scoreList.Add("Raie", new ScoreElement(500f));
        scoreList.Add("Blobfish", new ScoreElement(100f));
        scoreList.Add("BigOrno", new ScoreElement(300f));
        scoreList.Add("Baleine", new ScoreElement(1f));

        randomStatList = new Dictionary<string, RandomStat>();
        randomStatList.Add("Timer", new RandomStat());
        randomStatList.Add("Tirs Effectués", new RandomStat());
        randomStatList.Add("Tirs Réussis", new RandomStat());
        randomStatList.Add("Grenades Lancées", new RandomStat());
        randomStatList.Add("Grenades Réussies", new RandomStat());
        randomStatList.Add("Grenades Touchées", new RandomStat());
        randomStatList.Add("Missiles Détruits", new RandomStat());
        randomStatList.Add("Missiles Esquivés", new RandomStat());
        randomStatList.Add("Dégâts Subies", new RandomStat());
    }

    public void addScore(string scoreType)
    {
        scoreList[scoreType].incremente();
		score = calculScore();
		updateUI (score);
    }

    public void addStat(string statType)
    {
        randomStatList[statType].incremente();
    }



    public float calculScore()
    {
        float finalScore = 0;
        foreach (KeyValuePair<string, ScoreElement> scoreElement in scoreList)
        {
            finalScore += scoreElement.Value.getScore();
        }

        return finalScore;
    }

	public void updateUI(float score){
		scoreDisplayer.text = score.ToString ();
	}

    public string[] statToString()
    {
        if (scoreList == null)
        {
            setUp();
        }
        string[] stringed = new string[randomStatList.Count];
        int i = 0;
        foreach (KeyValuePair<string, RandomStat> statElement in randomStatList)
        {
            stringed[i] = statElement.Key + " : " + statElement.Value.getStat();
            i++;
        }
        return stringed;
    }
    public string[] scoreToString()
    {
        if (scoreList == null)
        {
            setUp();
        }
        string[] stringed = new string[scoreList.Count];
        int i = 0;
        foreach (KeyValuePair<string, ScoreElement> scoreElement in scoreList)
        {
            stringed[i] = scoreElement.Key + " : " + scoreElement.Value.getScore();
            i++;
        }

        return stringed;
    }
		
}
