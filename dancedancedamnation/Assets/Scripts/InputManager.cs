using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    public GameObject bar;

    public GameObject up;
    public GameObject down;
    public GameObject left;
    public GameObject right;

    public Animator upAnim;
    public Animator downAnim;
    public Animator leftAnim;
    public Animator rightAnim;



    [SerializeField]
    public PlayerAnimator playerAnim;

    public LayerMask m_LayerMask;

    private Collider2D[] hitColliders;
    private int combo;

    [SerializeField]
    TMPro.TextMeshProUGUI comboText;

    [SerializeField]
    private Animator accuracyAnim;

    [SerializeField]
    AccuracyText accuracyText;

    [SerializeField]
    ScoreController score;

    [SerializeField]
    ParanoiaController paranoia;

    [SerializeField]
    public AudioSource arrowSoundL;

    [SerializeField]
    public AudioSource arrowSoundR;

    [SerializeField]
    public AudioSource arrowSoundU;
    
    [SerializeField]
    public AudioSource arrowSoundD;
    
    [SerializeField]
    public AudioSource missSound;

    private bool needLeft, needRight, needUp, needDown;
    private bool presssedLeft, pressedRight, pressedUp, pressedDown;

    // Start is called before the first frame update
    void Start()
    {
        //arrowSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        comboText.text = combo.ToString();

        if (Input.anyKey)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                directionInput(Direction.Left);
                arrowSoundL.Play();
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                directionInput(Direction.Right);
                arrowSoundR.Play();
            }
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                directionInput(Direction.Up);
                arrowSoundU.Play();
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                directionInput(Direction.Down);
                arrowSoundD.Play();
            }
        }
    }

    void directionInput(Direction dir)
    {
        switch (dir)
        {
            case Direction.Up:
                hitColliders = Physics2D.OverlapBoxAll(up.transform.position, up.transform.localScale, 0, m_LayerMask);
                break;
            case Direction.Down:
                hitColliders = Physics2D.OverlapBoxAll(down.transform.position, down.transform.localScale, 0, m_LayerMask);
                break;
            case Direction.Left:
                hitColliders = Physics2D.OverlapBoxAll(left.transform.position, left.transform.localScale, 0, m_LayerMask);
                break;
            case Direction.Right:
                hitColliders = Physics2D.OverlapBoxAll(right.transform.position, right.transform.localScale, 0, m_LayerMask);
                break;
        }
        if (hitColliders.Length > 0)
        {
            foreach (Collider2D collider in hitColliders)
            {
                needLeft = false;
                needRight = false;
                needUp = false;
                needDown = false;

                MusicNote musicNote = collider.gameObject.GetComponent<MusicNote>();
                switch (musicNote.note.direction)
                {
                    case Direction.Up:
                        needUp = true;
                        break;
                    case Direction.Down:
                        needDown = true;
                        break;
                    case Direction.Left:
                        needLeft = true;
                        break;
                    case Direction.Right:
                        needRight = true;
                        break;
                }
            }
            foreach (Collider2D collider in hitColliders)
            {
                MusicNote musicNote = collider.gameObject.GetComponent<MusicNote>();
                Accuracy accuracy = musicNote.registerInput(dir);
                if(accuracy == Accuracy.Perfect)
                {
                    Perfect(musicNote);
                }
                Destroy(collider.gameObject); // Change to cool animation later
                accuracyText.Set(accuracy);
                accuracyAnim.SetTrigger("Set");
                score.UpdateScore(accuracy, combo);
            }
        }
        else
        {
            Miss();
        }
    }

    public void Miss()
    {
        // Paranoia is incremented in accuracy text
        accuracyText.Set(Accuracy.Miss);
        score.UpdateScore(Accuracy.Miss, 0);
        accuracyAnim.SetTrigger("Set");
        playerAnim.player.SetTrigger("Hurt");
        breakCombo();
    }

    public void Perfect(MusicNote mn)
    {
        switch (mn.note.direction)
        {
            case Direction.Left:
                leftAnim.SetTrigger("Perfect");
                break;
            case Direction.Down:
                downAnim.SetTrigger("Perfect");
                break;
            case Direction.Up:
                upAnim.SetTrigger("Perfect");
                break;
            case Direction.Right:
                rightAnim.SetTrigger("Perfect");
                break;
            default:
                break;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(bar.transform.position, bar.transform.localScale * 2);
    }

    public void incrementCombo()
    {
        combo++;
    }

    public void breakCombo()
    {
        combo = 0;
    }
}
