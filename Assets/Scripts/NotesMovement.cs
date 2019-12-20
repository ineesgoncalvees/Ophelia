using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesMovement : MonoBehaviour
{
    [SerializeField]
    private float velMov;
    [SerializeField]
    private AudioSource music;

    public bool isLeft;

    private bool starPlaying;
    private bool hasStarted;

    // Start is called before the first frame update
    void Start()
    {
        velMov = velMov / 60f;
        hasStarted = false;
        starPlaying = false;
    }

    private void Update()
    {
        PlayMusic();
        print(music.enabled);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveNotes();
    }

    private void MoveNotes()
    {
        if (isLeft)
        {
            if (!hasStarted)
            {
                if (Input.touchCount > 0 || Input.GetKeyDown("space"))
                {
                    hasStarted = true;
                    starPlaying = true;
                }
            }
            else
            {
                transform.position -= new Vector3(velMov * Time.fixedDeltaTime, 0f, 0f);
            }
        }
        else
        {
            if (!hasStarted)
            {
                if (Input.touchCount > 0 || Input.GetKeyDown("space"))
                {
                    hasStarted = true;
                }
            }
            else
            {
                transform.position -= new Vector3(-velMov * Time.fixedDeltaTime, 0f, 0f);
            }
        }
    }

    private void PlayMusic()
    {
        //if (starPlaying)
            music.Play();
    }
}
