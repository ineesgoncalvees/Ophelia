using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ophelia/BeatMap")]
public class BeatMap : ScriptableObject
{
    [System.Serializable]
    public struct Beat
    {
        public float    time;
        public bool     isHold;
        public int      slot;
        public bool     isRight;
    };

    public AudioClip    music;
    public float        musicOffset;
    public List<Beat>   beats;

    public void Clear()
    {
        beats = new List<Beat>();
    }
}
