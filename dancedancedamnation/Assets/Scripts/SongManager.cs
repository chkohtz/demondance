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


    public float shiftAmount;
    public float spacing;

    [SerializeField]
    AudioSource audioSource;

    public Song song;
    int nextIndex = 0;

    public GameObject notePrefab;

    public GameObject spawnPos;
    public GameObject endPos;

    private Queue<MusicNote> activeNotes;

    public bool wonGame;

    void Start()
    {
        songPosition = 0;
        songPosInBeats = 0;
        secPerBeat = 60f / song.bpm;
        dsptimesong = (float)AudioSettings.dspTime;
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    void Update()
    {
        if (wonGame)
        {
            return;
        }
        //debug spawn arrows
        //if(Input.GetKeyDown(KeyCode.J))
        //{
        //    MusicNote testNote = Instantiate(notePrefab).GetComponent<MusicNote>();
        //    testNote.note.direction = Direction.Left;           
        //    testNote.SpawnPos = spawnPos.transform.position;
        //    testNote.RemovePos= endPos.transform.position;
        //}

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

        if(songPosition > audioSource.clip.length)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        wonGame = true;
        UnityEngine.Debug.Log("You win!!!");
    }

    [ContextMenu("Shift Notes")]
    public void shiftNotes()
    {
        foreach (Note note in song.notes)
        {
            note.pos += shiftAmount;
        }
    }

    [ContextMenu("Space Notes")]
    public void spaceNotes()
    {
        foreach (Note note in song.notes)
        {
            note.pos *= spacing;
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