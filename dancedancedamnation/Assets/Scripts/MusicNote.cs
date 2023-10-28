using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MusicNote : MonoBehaviour
{
    public Note note;
    public Vector2 SpawnPos;
    public Vector2 RemovePos;
    public float BeatsShownInAdvance;
    private float beatOfThisNote;

    public float moveTime = 10;
    private float elapsedTime = 0;

    [SerializeField] private AnimationCurve curve;

    SongManager songManager;
    InputManager inputManager;

    private float noteSpacing = 1.5f;

    [SerializeField]
    Sprite upArrow;
    [SerializeField]
    Sprite downArrow;
    [SerializeField]
    Sprite leftArrow;
    [SerializeField]
    Sprite rightArrow;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        songManager = FindObjectOfType<SongManager>();
        inputManager = FindObjectOfType<InputManager>();
        beatOfThisNote = note.pos;
        sr = GetComponent<SpriteRenderer>();

        switch (note.direction)
        {
            case Direction.Left:
                sr.sprite = leftArrow;
                SpawnPos += new Vector2(-2 * noteSpacing, 0);
                RemovePos += new Vector2(-2 * noteSpacing, 0);
                break;
            case Direction.Down:
                sr.sprite = downArrow;
                SpawnPos += new Vector2(-1 * noteSpacing, 0);
                RemovePos += new Vector2(-1 * noteSpacing, 0);
                break;
            case Direction.Up:
                sr.sprite = upArrow;
                SpawnPos += new Vector2(1 * noteSpacing, 0);
                RemovePos += new Vector2(1 * noteSpacing, 0);
                break;
            case Direction.Right:
                sr.sprite = rightArrow;
                SpawnPos += new Vector2(2 * noteSpacing, 0);
                RemovePos += new Vector2(2 * noteSpacing, 0);
                break;
            default:
                // code block
                break;
        }

        transform.position = SpawnPos;

    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / moveTime;
        transform.position = Vector2.Lerp(SpawnPos, RemovePos, elapsedTime*0.5f);

        if(transform.position.Equals(RemovePos))
        {
            Destroy(gameObject);
            inputManager.breakCombo();
        }
    }

    public Accuracy registerInput(Direction dir)
    {
        if(dir == note.direction)
        {
            inputManager.incrementCombo();
            float distance = Mathf.Abs(transform.position.y - inputManager.bar.transform.position.y);
            if (distance < 0.1)
            {
                return Accuracy.Perfect;
            }
            else if (distance < 0.2)
            {
                return Accuracy.Great;
            }
            else if (distance < 0.3)
            {
                return Accuracy.Good;
            }
            else
            {
                return Accuracy.Okay;
            }
        }
        else
        {
            inputManager.breakCombo();
            return Accuracy.Miss;
        }
    }

    
}
