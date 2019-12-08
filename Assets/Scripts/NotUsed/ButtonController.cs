using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    //[SerializeField]
    //private Sprite defaultImage;
    //[SerializeField]
    //private Sprite pressedImage;

    //private SpriteRenderer sr;

    //void Start()
    //{
    //    sr = GetComponent<SpriteRenderer>();
    //}

    //void OnTouchDown()
    //{
    //    sr.sprite = pressedImage;
    //}
    //void OnTouchUp()
    //{
    //    sr.sprite = defaultImage;
    //}
    //void OnTouchStay()
    //{
    //    sr.sprite = pressedImage;
    //}
    //void OnTouchHold()
    //{
    //    sr.sprite = pressedImage;
    //}
    //void OnTouchExit()
    //{
    //    sr.sprite = defaultImage;
    //}




    [SerializeField]
    private Sprite defaultImage;
    [SerializeField]
    private Sprite pressedImage;

    private SpriteRenderer sr;
    private Touch touch;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void ButtonPush()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            sr.sprite = pressedImage;
        }

        if (touch.phase == TouchPhase.Ended)
        {
            sr.sprite = defaultImage;
        }
    }
}
