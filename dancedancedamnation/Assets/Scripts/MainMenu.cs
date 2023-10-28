using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartIntro()
    {
        SceneManager.LoadSceneAsync("Cutscenes");
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync("Game");
    }
}