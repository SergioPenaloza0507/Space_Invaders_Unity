using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private Score _score;

    private static ScoreManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
        }
        else if (instance != this)
        {
            Destroy(this);
        }
        _score = new Score();
    }
    
    public void Score(float plus)
    {
        _score.amount += plus;
    }

    public void SaveHiScore()
    {
        _score.hiScores.Add(_score.amount);
    }

    public static ScoreManager Instance => instance;
    public Score sc => _score;
}

[System.Serializable]
public class Score
{
    public float amount;

    public List<float> hiScores;
}
