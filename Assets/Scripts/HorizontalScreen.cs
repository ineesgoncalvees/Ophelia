﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalScreen : MonoBehaviour
{
    void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
}
