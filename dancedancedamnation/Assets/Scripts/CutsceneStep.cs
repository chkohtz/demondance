using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CutsceneData", menuName = "Cutscene", order = 2)]
[System.Serializable]
public class Cutscene : ScriptableObject
{
    public List<CutsceneStep> steps;
}