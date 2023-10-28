using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public Animator enemy;
    public bool canDance;
    public float danceCooldown;

    public string[] danceTriggers = { "left", "right", "up", "down" };

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Animator>();
        canDance = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canDance)
        {
            canDance = false;
            StartCoroutine(Dance());
        }
    }

    IEnumerator Dance()
    {
        string trigger = danceTriggers[UnityEngine.Random.Range(0, 4)];
        enemy.SetTrigger(trigger);
        UnityEngine.Debug.Log(trigger);
        yield return new WaitForSeconds(danceCooldown);
        canDance = true;
    }
}
