using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CutsceneController : MonoBehaviour
{
    [SerializeField]
    public Cutscene cutscene;
    [SerializeField]
    public Image[] images;
    private Image currentImage;
    [SerializeField]
    public DialogueManager dialogueManager;
    [SerializeField]
    public Image bgImage;


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
        waitTime -= Time.deltaTime;

        if(waitTime <= 0)
        {
            AdvanceStep();
        }
    }

    void AdvanceStep()
    {
        paused = false;
        if (stepIndex >= cutscene.steps.Count)
        {
            //TODO: end cutscene
            return;
        }
        CutsceneStep step = cutscene.steps[stepIndex];

        switch (step.type) {
            case StepType.Dialog:
                paused = true;
                dialogueManager.StartConversation(step.dialogue);
                break;
            case StepType.ImageChange:
                bgImage.sprite = step.image;
                break;
            case StepType.Wait:
                waitTime = step.waitTime;
                break;
        }

        stepIndex++;
    }

    public void OnDialogueFinish()
    {
        AdvanceStep();
    }
}

[CreateAssetMenu(fileName = "CutsceneData", menuName = "Cutscene", order = 2)]
[System.Serializable]
public class Cutscene : ScriptableObject
{
    public List<CutsceneStep> steps;
}


[System.Serializable]
public class CutsceneStep 
{
    public StepType type;
#nullable enable
    public Conversation? dialogue;
    public Sprite? image;
#nullable disable
    public double waitTime;
}

public enum StepType
{
    Dialog,
    ImageChange,
    Wait,
}