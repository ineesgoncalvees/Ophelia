using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivateParticules : MonoBehaviour
{
    private Touch touch;

    [SerializeField]
    private GameObject particules;

    private void Update()
    {
        if (touch.tapCount > 0)
        {
            particules.SetActive(true);
            particules.transform.position = touch.position;

        }
        else if (touch.tapCount == 0)
        {
            particules.SetActive(false);
        }
    }
}
