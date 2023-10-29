using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyText : MonoBehaviour
{

    private SpriteRenderer sr;

    public Sprite miss;
    public Sprite okay;
    public Sprite good;
    public Sprite great;
    public Sprite perfect;

    [SerializeField]
    ParanoiaController paranoia;
    [SerializeField]
    ScoreController score;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set(Accuracy accuracy)
    {
        switch (accuracy)
        {
            case Accuracy.Miss:
                sr.sprite = miss;
                paranoia.incrementValue(20.0f);
                break;
            case Accuracy.Okay:
                sr.sprite = okay;
                paranoia.incrementValue(10f);
                break;
            case Accuracy.Good:
                sr.sprite = good;
                paranoia.incrementValue(5f);
                break;
            case Accuracy.Great:
                sr.sprite = great;
                paranoia.incrementValue(-10f);
                break;
            case Accuracy.Perfect:
                sr.sprite = perfect;
                paranoia.incrementValue(-30f);
                break;
        }
    }
}
