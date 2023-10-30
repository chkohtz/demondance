using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour
{
    [SerializeField]
    public List<CutsceneStep> cutsceneList;
    private Image currentImage;
    [SerializeField]
    public DialogueManager dialogueManager;
    [SerializeField]
    public Image bgImage;

    [SerializeField]
    GameObject inputField;

    private int cutsceneIndex = 0;

    private string input;
    bool receivingInput;


    private int stepIndex = 0;
    private bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        receivingInput = false;
        inputField.SetActive(false);
        dialogueManager.SetState(false);
        AdvanceStep();
    }

    private void Update()
    {
        if (paused) return;

        if (!receivingInput)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && dialogueManager.isFinished && stepIndex <= cutsceneList.Count)
                AdvanceStep();
        }
    }

    void AdvanceStep()
    {
        Debug.Log("advance step: " + stepIndex);

        paused = false;
        if (stepIndex >= cutsceneList.Count)
        {
            //TODO: end cutscene
            stepIndex = 0;
            cutsceneIndex++;
            Debug.Log("OwO");
            return;
        }
        CutsceneStep step = cutsceneList[stepIndex];

        stepIndex++;

        if (step.audioSource != null)
            step.audioSource.Play();

        switch (step.type) {
            case StepType.Dialog:
                paused = true;
                dialogueManager.SetState(true);
                dialogueManager.StartConversation(step.dialogue);
                if(step.clip != null)
                    bgImage.GetComponent<Animator>().Play(step.clip.name);
                break;
            case StepType.ImageChange:
                dialogueManager.SetState(false);
                dialogueManager.isFinished = true;
                bgImage.GetComponent<Animator>().Play(step.clip.name);
                break;
            case StepType.Input:
                stepIndex--;
                receivingInput = true;
                dialogueManager.SetState(false);
                inputField.SetActive(true);
                break;
            case StepType.SceneChange:
                dialogueManager.SetState(false);
                inputField.SetActive(false);
                SceneManager.LoadSceneAsync(step.sceneName);
                break;
        }

        
    }

    public void OnDialogueFinish()
    {
        AdvanceStep();
    }

    public void getInput()
    {
        input = inputField.GetComponent<TMP_InputField>().text;
        inputField.SetActive (false);
        receivingInput = false;

        CutsceneStep step = cutsceneList[stepIndex];
        paused = true;
        dialogueManager.SetState(true);
        if(step.inputMatch.Equals(input))
            dialogueManager.StartConversation(step.dialogue);
        else
            dialogueManager.StartConversation(step.dialogueAlt);
        if (step.clip != null)
            bgImage.GetComponent<Animator>().Play(step.clip.name);
        stepIndex++;

    }
}

[System.Serializable]
public class CutsceneStep 
{
    public StepType type;
#nullable enable
    public DialogueSequence? dialogue;
    public DialogueSequence? dialogueAlt;
    public string? inputMatch;
    public AnimationClip? clip;
#nullable disable
    public AudioSource audioSource;
    public string sceneName;
    public bool screenShake;
}

public enum StepType
{
    Dialog,
    ImageChange,
    Input,
    SceneChange
}