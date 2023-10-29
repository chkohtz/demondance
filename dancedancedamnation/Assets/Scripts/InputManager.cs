using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    public GameObject bar;

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

    // Start is called before the first frame update
    void Start()
    {
        //arrowSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        comboText.text = combo.ToString();        

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

    void directionInput(Direction dir)
    {
        hitColliders = Physics2D.OverlapBoxAll(bar.transform.position, bar.transform.localScale, 0, m_LayerMask);
        Debug.Log(hitColliders.Length);
        if (hitColliders.Length > 0)
        {
            foreach (Collider2D collider in hitColliders)
            {
                Accuracy accuracy = collider.gameObject.GetComponent<MusicNote>().registerInput(dir);
                Destroy(collider.gameObject); // Change to cool animation later
                accuracyText.Set(accuracy);
                accuracyAnim.SetTrigger("Set");
                score.UpdateScore(accuracy, combo);
            }
        }
        else
        {
            accuracyText.Set(Accuracy.Miss);
            score.UpdateScore(Accuracy.Miss, 0);
            accuracyAnim.SetTrigger("Set");
            breakCombo();
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
