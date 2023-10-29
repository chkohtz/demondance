using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParanoiaController : MonoBehaviour
{
    public float maxVal = 100;
    public ParanoiaBar paranoiaBar;
    public GameOverController gameOverController;

    [SerializeField]
    public Crowd crowd;

    public float currentVal
    {
        get => _currentVal;
        set {
            _currentVal = Mathf.Clamp(value, 0, maxVal);
            paranoiaBar.SetValue(_currentVal);
        }
    }
    private float _currentVal = 0;


    // Start is called before the first frame update
    void Start()
    {
        currentVal = maxVal / 2;
    }


    public void incrementValue(float change)
    {
        currentVal += change;
        if(_currentVal >= maxVal)
        {
            gameOverController.OnGameOver();
        };
        if (currentVal <= 0)
        {
            crowd.Cheer();
        }
    }
}
