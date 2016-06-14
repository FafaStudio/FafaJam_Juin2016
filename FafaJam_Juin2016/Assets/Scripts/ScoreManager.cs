using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        int value;
        public RandomStat()
        {
            this.value = 0;
        }
        public int getStat()
        {
            return this.value;
        }
        public void incremente()
        {
            this.value++;
        }
    }




    Dictionary<string, ScoreElement> scoreList;
    Dictionary<string, RandomStat> randomStatList;
    public float score;

    // Use this for initialization
    void Start () {
        if (scoreList == null)
        {
            setUp();
        }
    }

    private void setUp()
    {
        scoreList = new Dictionary<string, ScoreElement>();
        scoreList.Add("BasicEnemy", new ScoreElement(1f));
        scoreList.Add("Raie", new ScoreElement(1f));
        scoreList.Add("Blobfish", new ScoreElement(1f));
        scoreList.Add("Baleine", new ScoreElement(1f));

        randomStatList = new Dictionary<string, RandomStat>();
        randomStatList.Add("Tirs Effectués", new RandomStat());
        randomStatList.Add("Tirs Réussis", new RandomStat());
        randomStatList.Add("Grenades Lancées", new RandomStat());
        randomStatList.Add("Grenades Réussies", new RandomStat());
        randomStatList.Add("Grenades Touchées", new RandomStat());
        randomStatList.Add("Dégâts Subies", new RandomStat());
    }

    public void addScore(string scoreType)
    {
        scoreList[scoreType].incremente();
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

    // Update is called once per frame
    void FixedUpdate () {
        score = calculScore();
	}
}
