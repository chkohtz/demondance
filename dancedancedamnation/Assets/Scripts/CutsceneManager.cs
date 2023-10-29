using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using System.Security.Cryptography;
using System.Collections.Specialized;
using System.Threading;
using System;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> cutsceneList;


    private int index = 0;
    private GameObject image;

    // Start is called before the first frame update
    void Start()
    {
        PlayNext();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            image.SetActive(false);
            PlayNext();
        }
    }

    private void PlayNext()
    {
        if (index < cutsceneList.Count)
        {
            image = cutsceneList[index];
            image.SetActive(true);
            image.GetComponent<Animator>().Play("animation");
            index++;
        }
    }

    
}
