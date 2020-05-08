using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMove : MonoBehaviour
{
    public float speed;

    void FixedUpdate()
    {
        var pos = transform.position;
        pos.x += speed * Time.fixedDeltaTime;
        transform.position = pos;
    }
}
