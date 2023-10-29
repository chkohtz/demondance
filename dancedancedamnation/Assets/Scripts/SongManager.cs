using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.TerrainTools;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    [Header("Song Info")]
    public float songPosition;
    public float songPosInBeats;
    public float secPerBeat;
    public float dsptimesong;
    public float beatsShownInAdvance;
    public Beatmap beatmap;

    [Header("Edit Song")]
    public float shiftAmount;
    public float spacing;
    public Song song;

    [Header("Other")]

    [SerializeField]
    AudioSource audioSource;

    
    int nextIndex = 0;

    public GameObject notePrefab;

    public GameObject spawnPos;
    public GameObject endPos;

    private Queue<MusicNote> activeNotes;

    public bool wonGame;

    public GameOverController gameOverController;
    public CutsceneController cutsceneControl;
    public Animator stan;
    public DialogueManager dialogueManager;

    void Start()
    {
        songPosition = 0;
        songPosInBeats = 0;
        secPerBeat = 60f / beatmap.bpm;
        dsptimesong = (float)AudioSettings.dspTime;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = beatmap.audioClip;
        audioSource.Play();
    }

    void Update()
    {
        if (wonGame || gameOverController.game_over)
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

        if (nextIndex < beatmap.notes.Length && beatmap.notes[nextIndex].pos < songPosInBeats + beatsShownInAdvance)
        {
            MusicNote spawnedNote = Instantiate(notePrefab).GetComponent<MusicNote>();
            spawnedNote.note = beatmap.notes[nextIndex];
            spawnedNote.BeatsShownInAdvance = beatsShownInAdvance;
            spawnedNote.SpawnPos = spawnPos.transform.position;
            spawnedNote.RemovePos = endPos.transform.position;

            //activeNotes.Enqueue(spawnedNote);
            //initialize the fields of the music note

            nextIndex++;
        }

        if (songPosition > audioSource.clip.length)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        wonGame = true;
        UnityEngine.Debug.Log("You win!!!");
        cutsceneControl.gameObject.SetActive(true);
        dialogueManager.gameObject.SetActive(true);
        stan.SetBool("dead", true);

    }

    [ContextMenu("Shift Notes (Beatmap)")]
    public void shiftNotes()
    {
        foreach (Note note in beatmap.notes)
        {
            note.pos += shiftAmount;
        }
        Debug.Log("Beatmap shifted by " + shiftAmount);
    }

    [ContextMenu("Space Notes (Beatmap)")]
    public void spaceNotes()
    {
        foreach (Note note in beatmap.notes)
        {
            note.pos *= spacing;
        }
        Debug.Log("Beatmap spaced out by a factor of" + shiftAmount);
    }

    [ContextMenu("Convert Song (old) to Beatmap")]
    public void convert()
    {
        beatmap.bpm = song.bpm;
        beatmap.audioClip = song.audioClip;
        beatmap.notes = song.notes;
        Debug.Log("Song " + song.name + " successfully copied into Beatmap " + beatmap.name);
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