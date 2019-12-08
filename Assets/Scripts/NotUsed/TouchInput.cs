using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{
    public LayerMask touchInputMask;

    private List<GameObject> touchList = new List<GameObject>();
    private GameObject[] touchesOld;

    private RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touchesOld = new GameObject[touchList.Count];
            touchList.CopyTo(touchesOld);
            touchList.Clear();

            foreach (Touch touch in Input.touches)
            {
                Vector3 wrlPos = GetComponent<Camera>().ScreenToWorldPoint(touch.position);

                Ray ray = GetComponent<Camera>().ScreenPointToRay(wrlPos);                

                Debug.DrawRay(touch.position, transform.forward, Color.red);
                print(wrlPos);

                if (Physics.Raycast(ray, out hit, touchInputMask))
                {
                    Debug.Log("entrei");
                    GameObject recipient = hit.transform.gameObject;
                    touchList.Add(recipient);
                    
                    if (touch.phase == TouchPhase.Began)
                        recipient.SendMessage("OnTouchDown", hit.point,
                            SendMessageOptions.DontRequireReceiver);

                    if (touch.phase == TouchPhase.Ended)
                        recipient.SendMessage("OnTouchUp", hit.point,
                            SendMessageOptions.DontRequireReceiver);

                    if (touch.phase == TouchPhase.Stationary)
                        recipient.SendMessage("OnTouchStay", hit.point,
                            SendMessageOptions.DontRequireReceiver);

                    if (touch.phase == TouchPhase.Moved)
                        recipient.SendMessage("OnTouchHold", hit.point,
                            SendMessageOptions.DontRequireReceiver);

                    if (touch.phase == TouchPhase.Canceled)
                        recipient.SendMessage("OnTouchExit", hit.point,
                            SendMessageOptions.DontRequireReceiver);
                }
            }

            foreach (GameObject g in touchesOld)
            {
                if (!touchList.Contains(g))
                    g.SendMessage("OnTouchExit", hit.point,
                            SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
