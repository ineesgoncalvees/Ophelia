using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesMovement : MonoBehaviour
{
    [SerializeField]
    private float velMov;
    public bool isLeft;

    private bool hasStarted;

    // Start is called before the first frame update
    void Start()
    {
        velMov = velMov / 60f;
    }

    // Update is called once per frame
    void Update()
    {
        MoveNotes();
    }

    private void MoveNotes()
    {
        if (isLeft)
        {
            if (!hasStarted)
            {
                //if (Input.touchCount > 0)
                if (Input.GetKeyDown("space"))
                {
                    hasStarted = true;
                }
            }
            else
            {
                transform.position -= new Vector3(velMov * Time.deltaTime, 0f, 0f);
            }
        }
        else
        {
            if (!hasStarted)
            {
                //if (Input.touchCount > 0)
                if (Input.GetKeyDown("space"))
                {
                    hasStarted = true;
                }
            }
            else
            {
                transform.position -= new Vector3(-velMov * Time.deltaTime, 0f, 0f);
            }
        }
    }


}
