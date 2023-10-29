using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using System.Security.Cryptography;
using System.Collections.Specialized;
using System.Threading;

public class CutsceneManager : MonoBehaviour
{
    public PlayableDirector pDirector;
    public GameObject image;
    public float startTime;
    public Vector3 startPos;
    public Vector3 endPos;
    public float elapsedTime = 0;
    public float moveTime = 5f;
    [SerializeField] private AnimationCurve curve;

    // Start is called before the first frame update
    void Start()
    {
        startPos = image.transform.position;
        
        endPos = image.transform.position - new Vector3(0, -100, 0);
        startTime = Time.time;
        StartCoroutine(CameraPan());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CameraPan()
    {
        while(image.transform.position != endPos)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / moveTime;
            image.transform.position = Vector3.Lerp(startPos, endPos, curve.Evaluate(percentageComplete));

            image.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime*0.5f);
            yield return null;
        }

    }
}
