using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public BeatMap beatMap;
    public float notePrevTime = 2;

    public RectTransform[]  noteStart;
    public RectTransform[]  noteEndRight;
    public RectTransform[]  noteEndLeft;

    public NoteMove         notePrefab;
    public RectTransform    noteCanvas;

    float   startTime = 0.0f;
    int     beatIndex = 0;
    bool    isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        isPlaying = true;

        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = beatMap.music;
        audioSource.Play();

        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            float elapsedTime = Time.time - startTime - notePrevTime;

            while (true)
            {
                if (beatMap.beats[beatIndex].time <= elapsedTime)
                {
                    var beat = beatMap.beats[beatIndex];

                    NoteMove note = Instantiate(notePrefab, noteStart[beat.slot].position, Quaternion.identity);
                    note.transform.SetParent(noteCanvas);

                    if (beat.isRight)
                        note.speed = (noteEndRight[beat.slot].position.x - noteStart[beat.slot].position.x) / notePrevTime;
                    else
                        note.speed = (noteEndLeft[beat.slot].position.x - noteStart[beat.slot].position.x) / notePrevTime;

                    beatIndex++;

                    if (beatMap.beats.Count <= beatIndex)
                    {
                        beatIndex = 0;
                        startTime = Time.time;
                        isPlaying = false;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }
    }
}
