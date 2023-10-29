using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog (Conversation)")]
[Serializable]
public class Dialog : ScriptableObject
{
    public List<Dialogue> lines;
}