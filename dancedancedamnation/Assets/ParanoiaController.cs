using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParanoiaController : MonoBehaviour
{
    public float currentVal = 0;

    public ParanoiaBar paranoiaBar;

    // Start is called before the first frame update
    void Start()
    {
        
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

        currentVal = Mathf.Clamp(currentVal, 0, 100);

        paranoiaBar.SetValue(currentVal);
    }
}
