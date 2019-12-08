using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
}
