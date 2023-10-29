using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour
{
    [SerializeField]
    public List<Cutscene> cutsceneList;
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
            if (Input.GetKeyDown(KeyCode.Space) && dialogueManager.isFinished && stepIndex <= cutsceneList[cutsceneIndex].steps.Count)
                AdvanceStep();
        }
    }

    void AdvanceStep()
    {
        Debug.Log("advance step: " + stepIndex);

        paused = false;
        if (stepIndex >= cutsceneList[cutsceneIndex].steps.Count)
        {
            //TODO: end cutscene
            stepIndex = 0;
            cutsceneIndex++;
            Debug.Log("OwO");
            return;
        }
        CutsceneStep step = cutsceneList[cutsceneIndex].steps[stepIndex];

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