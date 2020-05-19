using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyNotes : MonoBehaviour
{
    private GameObject note;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Notes")
        {
            note = other.gameObject;
            Destroy(note);
        }
    }
}
