using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe que define a orientação do ecrâ
/// </summary>
public class HorizontalScreen : MonoBehaviour
{
    /// <summary>
    /// Metodo Awake() que corre no principio o programa
    /// </summary>
    void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
}
