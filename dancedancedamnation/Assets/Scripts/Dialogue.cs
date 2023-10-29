using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
[Serializable]
public class Dialogue
{
    public Sprite portrait;
    public string name;

    [TextArea(3, 10)]
    public string line;
}
