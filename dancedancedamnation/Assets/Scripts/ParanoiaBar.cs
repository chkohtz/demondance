using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParanoiaBar : MonoBehaviour
{
    public Slider barSlider;
    
    // Start is called before the first frame update
    void Start()
    {
        barSlider = GetComponent<Slider>();
        barSlider.maxValue = 100;
        barSlider.value = 0;
    }

    public void SetValue(float value)
    {
        barSlider.value = value;
    }


}
