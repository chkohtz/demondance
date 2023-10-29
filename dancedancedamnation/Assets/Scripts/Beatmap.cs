using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Beatmap", menuName = "Beatmap", order = 1)]
public class Beatmap : ScriptableObject
{
    public float bpm;
    public AudioClip audioClip;
    [SerializeField]
    public Note[] notes;


}
