using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SongMap", menuName = "SongMap", order = 1)]
public class SongMap : ScriptableObject
{
    public float bpm;
    public AudioClip audioClip;
    [SerializeField]
    public Note[] notes;


}