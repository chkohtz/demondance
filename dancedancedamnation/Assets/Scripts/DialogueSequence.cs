using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Sequence", menuName = "Dialogue Sequence")]
[Serializable]
public class DialogueSequence : ScriptableObject
{
    public List<Dialogue> lines;
}