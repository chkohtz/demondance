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
    [SerializeField]
    private TextMeshProUGUI scoreChangeTextbox;

    // Start is called before the first frame update
    void Start()
    {
        scoreTextbox.text = score.ToString();
        scoreChangeTextbox.alpha = 0;
    }

    private void Update()
    {
        scoreChangeTextbox.alpha -= 2f * Time.deltaTime;
    }

    void AddScore(int value) {
        score += value;
    }

    public void UpdateScore(Accuracy accuracy, int combo)
    {
        double scoreBonus = 0;
        string color = "#FFFFFF";
        switch (accuracy) {
            case Accuracy.Miss:
                scoreBonus = MissScore;
                color = "#FF0000";
                break;
            case Accuracy.Okay:
                scoreBonus = OkayScore;
                color = "#88FF00";
                break;
            case Accuracy.Good:
                scoreBonus = GoodScore;
                color = "#00FF00";
                break;
            case Accuracy.Great:
                scoreBonus = GreatScore;
                color = "#0000FF";
                break;
            case Accuracy.Perfect:
                scoreBonus = PerfectScore;
                color = "#7700FF";
                break;
        }

        scoreBonus = Math.Round(scoreBonus * (1 + (combo / 50.0)));
        score += Convert.ToInt32(scoreBonus);

        scoreChangeTextbox.alpha = 1;
        scoreChangeTextbox.text = $"<color={color}>" + scoreBonus.ToString("+#;-#") + "</color>";
    }
}
