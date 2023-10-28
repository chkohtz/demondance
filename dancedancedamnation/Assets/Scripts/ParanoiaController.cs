using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParanoiaController : MonoBehaviour
{
    public float currentVal = 0;
    public float maxVal = 100;

    public ParanoiaBar paranoiaBar;

    // Start is called before the first frame update
    void Start()
    {
        currentVal = maxVal / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            currentVal += 5f;
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)) {
            currentVal -= 5f;
        }

        currentVal = Mathf.Clamp(currentVal, 0, maxVal);

        paranoiaBar.SetValue(currentVal);
    }

    public void incrementValue(float change)
    {
        currentVal += change;
        if(currentVal >= maxVal)
        {

        }
        else if(currentVal <= 0)
        {
            currentVal = 0;
        }
    }
}
