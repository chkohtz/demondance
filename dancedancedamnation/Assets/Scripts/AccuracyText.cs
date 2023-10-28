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
                break;
            case Accuracy.Okay:
                sr.sprite = okay;
                break;
            case Accuracy.Good:
                sr.sprite = good;
                break;
            case Accuracy.Great:
                sr.sprite = great;
                break;
            case Accuracy.Perfect:
                sr.sprite = perfect;
                break;
            default: 
                sr.sprite = miss;
                break;
        }
    }
}
