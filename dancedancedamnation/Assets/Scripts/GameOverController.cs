using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool game_over;

    public SongManager songManager;
    public AudioClip scratch;
    public AudioClip boos;
    public Animator playerAnimator;

    public Animator enemyAnimator;

    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (game_over)
            gameObject.SetActive(true);
    }

    public void OnGameOver()
    {

        this.gameObject.SetActive(true);
        if (!game_over)
        {
            game_over = true;
            
            // handle music notes
            
            
            transform.localScale.Set(1, 1, 0);
            songManager.GetComponent<AudioSource>().Stop();
            songManager.GetComponent<AudioSource>().PlayOneShot(scratch);

            songManager.GetComponent<AudioSource>().PlayOneShot(boos);
            songManager.FadeNotes();

            playerAnimator.SetBool("lose", true);
            playerAnimator.speed = 0;
            enemyAnimator.speed = 0;

            //get a camera zoom
        }
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
