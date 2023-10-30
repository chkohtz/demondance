using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beatmap : MonoBehaviour
{
    [Header("Beatmap")]
    public AudioClip audioClip;
    public float bpm;
    [SerializeField]
    public Note[] notes;

    [Header("Edit")]
    public float shiftAmount;
    public float spacing;

    [ContextMenu("Shift Notes (Beatmap)")]
    public void shiftNotes()
    {
        foreach (Note note in notes)
        {
            note.pos += shiftAmount;
        }
        Debug.Log("Beatmap shifted by " + shiftAmount);
    }

    [ContextMenu("Space Notes (Beatmap)")]
    public void spaceNotes()
    {
        foreach (Note note in notes)
        {
            note.pos *= spacing;
        }
        Debug.Log("Beatmap spaced out by a factor of" + shiftAmount);
    }

    public void writeToFile()
    {

    }

}
