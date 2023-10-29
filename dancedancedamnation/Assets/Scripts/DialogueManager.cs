using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

[CreateAssetMenu(fileName = "New Conversation", menuName = "Conversation")]
[Serializable]
public class Conversation : ScriptableObject
{
    public List<Dialogue> lines;
}

public class DialogueManager : MonoBehaviour
{
    public TMP_Text name;
    public TMP_Text boxText;
    public GameObject nameBox;
    public GameObject textBox;
    public Image portrait;

    public List<Conversation> conversations;
    public Conversation activeConvo;

    public bool canAdvance;
    public bool isFinished;
    int index = 0;

    public AudioSource audio;
    public AudioClip clip;

    public bool autoStart = false;

    // Start is called before the first frame update
    void Start()
    {
        isFinished = true;
        if(autoStart) StartDialogue(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canAdvance)
        {
            index++;
            if (index >= activeConvo.lines.Count)
            {
                isFinished=true;
                EndDialogue();
                return;
            }
            canAdvance = false;
            NextLine();
        }
    }

    public void StartDialogue(int convoIndex)
    {
        isFinished = false;
        SetState(true);
        index = 0;
        activeConvo = conversations[convoIndex];
        NextLine();
    }

    public void StartConversation(Conversation conversation)
    {
        isFinished = false;
        SetState(true);
        index = 0;
        activeConvo = conversation;
        NextLine();
    }

    void NextLine()
    {
        portrait.sprite = activeConvo.lines[index].portrait;
        boxText.text = "";
        StartCoroutine(TypeLine(activeConvo.lines[index]));
        name.text = activeConvo.lines[index].name;
    }

    IEnumerator TypeLine(Dialogue dialogue)
    {
        foreach (char c in dialogue.line.ToCharArray())
        {
            boxText.text += c;
            if(clip && c != ' ' && c!= '\n')
            {
                audio.PlayOneShot(clip);
            }
            yield return new WaitForSeconds(.03f);
        }
        canAdvance = true;
    }

    public void SetState(bool state)
    {
        nameBox.SetActive(state);
        textBox.SetActive(state);
        portrait.enabled = state;
    }

    void EndDialogue()
    {
        canAdvance = false;
        SetState(false);

        gameObject.SendMessageUpwards("OnDialogueFinish");
    }
}
