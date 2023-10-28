using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{

    private int _score = 0;
    public int score {
        get => _score;
        set {
            _score = value;
            scoreTextbox.text = score.ToString();
        }
    }

    public int MissScore = -25;
    public int OkayScore = 25;
    public int GoodScore = 50;
    public int GreatScore = 75;
    public int PerfectScore = 100;

    //Bonus for combos will be 1+Combo/Factor
    public int ComboFactor = 50;

    [SerializeField]
    private TextMeshProUGUI scoreTextbox;

    // Start is called before the first frame update
    void Start()
    {
        scoreTextbox.text = score.ToString();
    }

    void AddScore(int value) {
        score += value;
    }

    public void UpdateScore(Accuracy accuracy, int combo)
    {
        double scoreBonus = 0;
        switch (accuracy) {
            case Accuracy.Miss:
                scoreBonus = MissScore;
                break;
            case Accuracy.Okay:
                scoreBonus = OkayScore;
                break;
            case Accuracy.Good:
                scoreBonus = GoodScore;
                break;
            case Accuracy.Great:
                scoreBonus = GreatScore;
                break;
            case Accuracy.Perfect:
                scoreBonus = PerfectScore;
                break;
        }

        scoreBonus *= 1 + (combo / 50.0);
        score += Convert.ToInt32(scoreBonus);
    }
}
