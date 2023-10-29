using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CutsceneController : MonoBehaviour
{
    [SerializeField]
    public Cutscene cutscene;
    private Image currentImage;
    [SerializeField]
    public DialogueManager dialogueManager;
    [SerializeField]
    public Image bgImage;



    private string input;


    private int stepIndex = 0;
    private double waitTime = 0;
    private bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager.SetState(false);
        AdvanceStep();
    }

    private void Update()
    {
        if (paused) return;

        if(Input.GetKeyDown(KeyCode.Space) && dialogueManager.isFinished && stepIndex <= cutscene.steps.Count)
        {
            AdvanceStep();
        }
    }

    void AdvanceStep()
    {
        Debug.Log("advance step: " + stepIndex);

        paused = false;
        if (stepIndex >= cutscene.steps.Count)
        {
            //TODO: end cutscene
            Debug.Log("OwO");
            return;
        }
        CutsceneStep step = cutscene.steps[stepIndex];

        stepIndex++;

        if (step.audioSource != null)
            step.audioSource.Play();

        switch (step.type) {
            case StepType.Dialog:
                paused = true;
                dialogueManager.StartConversation(step.dialogue);
                if(step.clip != null)
                    bgImage.GetComponent<Animator>().Play(step.clip.name);
                break;
            case StepType.ImageChange:
                dialogueManager.isFinished = true;
                bgImage.GetComponent<Animator>().Play(step.clip.name);
                break;
            case StepType.Input:
                // input = 
                break;
        }

        
    }

    public void OnDialogueFinish()
    {
        AdvanceStep();
    }
}

[System.Serializable]
public class CutsceneStep 
{
    public StepType type;
#nullable enable
    public Conversation? dialogue;
#nullable disable
    public AnimationClip? clip;
    public AudioSource audioSource;
    public bool screenShake;
}

public enum StepType
{
    Dialog,
    ImageChange,
    Input,
}