using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SongManager : MonoBehaviour
{
    [Header("Song Info")]
    public float songPosition;
    public float songPosInBeats;
    public float secPerBeat;
    public float dsptimesong;
    public float beatsShownInAdvance;
    public Beatmap beatmap;

    [Header("Other")]

    private AudioSource audioSource;

    int nextIndex = 0;

    public GameObject notePrefab;
    public GameObject spawnPos;
    public GameObject endPos;

    public bool wonGame;

    public GameOverController gameOverController;
    public CutsceneController cutsceneControl;
    public Animator stan;
    public DialogueManager dialogueManager;

    public bool isStaan;

    void Start()
    {
        PlayBeatmap();
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


        if (isStaan)
        {
            stan.SetBool("dead", true);
            cutsceneControl.gameObject.SetActive(true);
            dialogueManager.gameObject.SetActive(true);
        }
        else
        {
            SceneManager.LoadSceneAsync("Staan");
        }

    }

    void PlayBeatmap()
    {
        songPosition = 0;
        songPosInBeats = 0;
        secPerBeat = 60f / beatmap.bpm;
        dsptimesong = (float)AudioSettings.dspTime;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = beatmap.audioClip;
        audioSource.Play();
    }
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