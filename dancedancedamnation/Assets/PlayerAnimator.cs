using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    public Animator player;
    public bool playerHurt;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Here for testing purposes
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.SetTrigger("Hurt");
            return;
        }

        //trigger anim for each direction
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            player.SetTrigger("up");
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            player.SetTrigger("down");
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            player.SetTrigger("left");
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            player.SetTrigger("right");
        }
    }
}

