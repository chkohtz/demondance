using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.TerrainTools;
using UnityEngine;

public class SongManager : MonoBehaviour
{

    public float songPosition;
    public float songPosInBeats;
    public float secPerBeat;
    public float dsptimesong;
    public float beatsShownInAdvance;

    [SerializeField]
    AudioSource audioSource;

    public Song song;
    int nextIndex = 0;

    public GameObject notePrefab;

    public GameObject spawnPos;
    public GameObject endPos;

    private Queue<MusicNote> activeNotes;

    void Start()
    {
        secPerBeat = 60f / song.bpm;
        dsptimesong = (float)AudioSettings.dspTime;
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    void Update()
    {
        songPosition = (float)(AudioSettings.dspTime - dsptimesong);
        songPosInBeats = songPosition / secPerBeat;

        if (nextIndex < song.notes.Length && song.notes[nextIndex].pos < songPosInBeats + beatsShownInAdvance)
        {
            MusicNote spawnedNote = Instantiate(notePrefab).GetComponent<MusicNote>();
            spawnedNote.note = song.notes[nextIndex];
            spawnedNote.BeatsShownInAdvance = beatsShownInAdvance;
            spawnedNote.SpawnPos = spawnPos.transform.position;
            spawnedNote.RemovePos= endPos.transform.position;

            //activeNotes.Enqueue(spawnedNote);
            //initialize the fields of the music note

            nextIndex++;
        }
    }
}

[CreateAssetMenu(fileName = "Data", menuName = "Song", order = 1)]
public class Song : ScriptableObject
{
    public float bpm;
    public AudioClip audioClip;
    [SerializeField]
    public Note[] notes;
}

[System.Serializable]
public class Note
{
    public float pos;
    public float holdDuration;
    public Direction direction;
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public enum Accuracy
{
    Miss,
    Okay,
    Good,
    Great,
    Perfect
}