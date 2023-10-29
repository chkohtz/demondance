using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogConverter : MonoBehaviour
{

    public Conversation conv;
    public Dialog dialog;

    [ContextMenu("onvert")]
    public void shiftNotes()
    {
        dialog.lines = conv.lines;
    }

}
