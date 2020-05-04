using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recorder : MonoBehaviour
{
    public AudioClip    music;
    public BeatMap      sequence;
    public Image[]      noteImage;

    bool        isRecording = false;
    AudioSource audioSource;
    float       startTime;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRecording)
        {
            foreach (var n in noteImage)
            {
                n.color = new Color(1, 1, 1, 0.2f);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isRecording = true;
                audioSource.clip = music;
                audioSource.Play();
                startTime = Time.time;
                sequence.Clear();
                sequence.music = music;

                foreach (var n in noteImage)
                {
                    n.color = Color.white;
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                BeatMap.Beat b = new BeatMap.Beat();
                b.time = Time.time - startTime;
                b.isHold = false;
                b.isRight = false;
                b.slot = 0;
                sequence.beats.Add(b);

                noteImage[0].color = Color.yellow;
            }
            else
            {
                noteImage[0].color = Color.Lerp(noteImage[0].color, Color.white, 0.1f);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                BeatMap.Beat b = new BeatMap.Beat();
                b.time = Time.time - startTime;
                b.isHold = false;
                b.isRight = false;
                b.slot = 1;
                sequence.beats.Add(b);

                noteImage[1].color = Color.yellow;
            }
            else
            {
                noteImage[1].color = Color.Lerp(noteImage[1].color, Color.white, 0.1f);
            }
        }
    }

#if UNITY_EDITOR
    private void OnDestroy()
    {
        UnityEditor.EditorUtility.SetDirty(sequence);
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
    }
#endif
}
