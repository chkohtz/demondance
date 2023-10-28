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
        }
    }

    public void registerInput(Direction dir)
    {
        if(dir == note.direction)
        {

        }
    }
}
