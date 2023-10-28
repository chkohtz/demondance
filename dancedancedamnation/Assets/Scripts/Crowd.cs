using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd : MonoBehaviour
{
    [SerializeField]
    public Animator crowd1;
    [SerializeField]
    public Animator crowd2;

    public float defaultSpeed = 1;
    public float cheerSpeed = 2;

    // Start is called before the first frame update
    void Start()
    {
        crowd1.speed = defaultSpeed;
        crowd2.speed = defaultSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cheer()
    {

    }

    IEnumerator StartCheer()
    {
        crowd1.speed = cheerSpeed;
        crowd2.speed = cheerSpeed;
        yield return new WaitForSeconds(2);
        crowd1.speed = defaultSpeed;
        crowd2.speed = defaultSpeed;

    }

}
