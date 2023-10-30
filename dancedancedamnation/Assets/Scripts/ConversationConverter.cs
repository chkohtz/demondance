using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ConversationConverter : MonoBehaviour
{
    [SerializeField]
    public Conversation[] conv;
    [SerializeField]
    public DialogueSequence[] sequence;

    [ContextMenu("Convert")]
    public void convert()
    {
        for (int i = 0; i < conv.Length; i++)
        {
            sequence[i].lines = conv[i].lines;
        }
    }
}
