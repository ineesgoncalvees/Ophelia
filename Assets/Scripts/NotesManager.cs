using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class NotesManager : MonoBehaviour
{
    [SerializeField]
    private bool isRight;
    //[SerializeField]
    //private Text fullCombo;


    private Button button;
    private GameObject currentNote;
    private Touch touch;
    private Vector2 startPos;
    private Vector2 direction;
    private float distance;
    private bool noMiss;
    private NotesMovement notesOrientation;

    private void Start()
    {
        button = GetComponent<Button>();
        noMiss = true;
        //fullCombo.GetComponent<Text>().enabled = false;
    }

    //private void Update()
    //{
    //    if (Input.touchCount > 0)
    //    {
    //        touch = Input.GetTouch(0);
    //        //DetectMove();
    //    }
    //    //else if (Input.GetKeyDown("space"))
    //        //DetectMove();
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Notes")
        {
            if (other.GetComponent<NoteBeahviour>().isRight != isRight) return;

            button.interactable = true;
            currentNote = other.gameObject;
            AddClickListener(currentNote);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (currentNote != null && currentNote.activeInHierarchy)
        {
            if (other.GetComponent<NoteBeahviour>().isRight != isRight) return;

            if ((other.tag == "Notes") && (currentNote.layer != LayerMask.NameToLayer("Hold")))
            {
                currentNote = null;
                GameManager.instance.NoteMiss();
                RemoveClickListener();
                noMiss = false;
            }
            //else if (currentNote.layer == LayerMask.NameToLayer("Hold"))
            //{
            //    currentNote.transform.GetChild(0).gameObject.SetActive(false);
            //}
        }
    }

    private void AddClickListener(GameObject note)
    {
        button.onClick.AddListener(() => { CheckHit(note); RemoveClickListener(); });
    }

    private void RemoveClickListener()
    {
        button.onClick.RemoveAllListeners();
    }

    private void CheckHit(GameObject note)
    {
        float notePosition = note.transform.parent.GetComponent<RectTransform>
            ().InverseTransformVector(note.transform.position).x;
        //float notePosition = note.transform.position.x;
        bool left = note.GetComponentInParent<NotesMovement>().isLeft;

        //if (!(currentNote.layer == LayerMask.NameToLayer("Hold")))
        {
            print(notePosition);
            button.transform.localScale = new Vector3(2f, 2f, 2f);
            note.SetActive(false);


            if (left)
            {
                if (notePosition < -19.9 || notePosition > -17.25)
                {
                    GameManager.instance.GoodHit();
                }
                else if ((notePosition > -19.9 && notePosition < -19.17) ||
                  (notePosition > -18 && notePosition < -17.25))
                {
                    GameManager.instance.GreatHit();
                }
                else if (notePosition > -19.17 && notePosition < -18)
                {
                    GameManager.instance.PerfectHit();
                }
            }
            else
            {
                if (notePosition < 16.34 || notePosition > 19)
                {
                    GameManager.instance.GoodHit();
                }
                else if ((notePosition > 16.34 && notePosition < 17.1) ||
                  (notePosition > 18.27 && notePosition < 19))
                {
                    GameManager.instance.GreatHit();
                }
                else if (notePosition > 17.1 && notePosition < 18.27)
                {
                    GameManager.instance.PerfectHit();
                }
            }

            button.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
    }

    // Metodo que ainda nao sei se vou usar, ainda estou a descobrir como fazer
    // esta parte

    //private void OnMouseUpAsButton()
    //{
    //    if (currentNote != null)
    //    {
    //        if (currentNote.layer == LayerMask.NameToLayer("Hold"))
    //        {
    //            //if(touch.phase == TouchPhase.Stationary)

    //            currentNote.transform.GetChild(0).gameObject.SetActive(false);
    //            GameManager.instance.NoteMiss();
    //        }
    //    }
    //}

    /// <summary>
    /// A ser implementado, ainda nao funciona
    /// </summary>
    private void DetectMove()
    {
        if (currentNote != null)
        {
            if (currentNote.layer == LayerMask.NameToLayer("Swipe"))
            {
                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    startPos = ray.GetPoint(distance);
                    //print(startPos);
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    Debug.Log("moveu");
                    direction = touch.position - startPos;
                    //print(direction);
                }
            }
        }
    }

    /// <summary>
    /// A ser implementado
    /// </summary>
    private void FullCombo()
    {
        //    noMiss = false;

        //    if (!noMiss)
        //    {
        //        fullCombo.GetComponent<Text>().enabled = true;
        //    }
    }

    /// <summary>
    /// A ser implementado
    /// </summary>
    private void AllPerfect()
    {
        // A ser implementado
    }
}
