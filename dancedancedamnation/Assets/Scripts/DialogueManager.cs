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
    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canAdvance)
        {

            index++;
            if (index >= activeConvo.lines.Count)
            {
                EndDialogue();
                return;
            }
            canAdvance = false;
            NextLine();
        }
    }

    void StartDialogue()
    {
        index = 0;
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
            yield return new WaitForSeconds(.025f);
        }
        canAdvance = true;
    }

    void EndDialogue()
    {
        nameBox.SetActive(false);
        textBox.SetActive(false);
        portrait.enabled = false;
    }
}
