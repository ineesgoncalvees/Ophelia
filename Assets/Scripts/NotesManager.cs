using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

/// <summary>
/// Classe que gere as diferentes notas
/// </summary>
public class NotesManager : MonoBehaviour
{
    // Variável booleana que diz que são as notas da direita ou da esquerda
    [SerializeField]
    private bool isRight;
    [SerializeField]
    private float minDistance;
    [SerializeField]
    private int iButton;

    private Button button;
    private GameObject currentNote;

    private bool firstHoldNoteHit;
    private bool secondHoldNoteHit;
    private bool isPressed;
    private float holdScore;

    [HideInInspector]
    public int currentButton;

    public static NotesManager instance;

    /// <summary>
    /// Método Start()
    /// </summary>
    private void Start()
    {
        instance = this;
        button = GetComponent<Button>();
    }

    private void Update()
    {
        if (isPressed)
        {
            if (!Input.GetMouseButton(0))
            {
                isPressed = false;
                CheckHit(currentNote);
            }
        }
    }

    /// <summary>
    /// Método chamado quando um collider 2D passa pelos botões que são trigger
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Se o collider tiver a tag de Notes
        if (other.tag == "Notes")
        {
            // Vai buscar a referência da nota que o ativou
            currentNote = other.gameObject;
            // Chama evento
            AddClickListener(currentNote);
        }
    }

    /// <summary>
    /// Método chamado quando um collider sai do trigger do botão
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other)
    {
        // Se houver uma currentNote e se estiver ativa
        if (currentNote != null && currentNote.activeInHierarchy)
        {
            if (other.GetComponent<NoteBeahviour>().isHold == true)
            {
                GameManager.instance.NoteMiss();
                currentNote = null;
                RemoveClickListener();
            }
            // Se for uma nota que não o hold
            else if ((other.tag == "Notes"))
            {
                // Devolve que o jogador falhou
                currentNote = null;
                GameManager.instance.NoteMiss();
                RemoveClickListener();
            }
        }
    }

    /// <summary>
    /// Método que atribui os delegates quando se carrega no botão
    /// </summary>
    /// <param name="note"></param>
    private void AddClickListener(GameObject note)
    {
        // Evento que aceita dois delegates
        button.onClick.AddListener(() => { CheckHit(note); RemoveClickListener(); });
    }

    /// <summary>
    /// Método que chama o evento para remover os listeners
    /// </summary>
    private void RemoveClickListener()
    {
        button.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// Método que define a acuracy do jogador
    /// </summary>
    /// <param name="note"></param>
    public void CheckHit(GameObject note)
    {
        if (currentNote != null)
        {
            if (currentNote.GetComponent<NoteBeahviour>().isHold == false)
            {
                note.SetActive(false);

                float distance = Vector3.Distance(note.transform.position, transform.position);
                float score = 0;

                if (distance < minDistance)
                {
                    score = NoteHit(distance);
                }

                GameManager.instance.currentScore += (int)score;
                GameManager.instance.UpdateScore();
            }
            else if(currentNote.GetComponent<NoteBeahviour>().isHold == true)
            {
                print("got it 1");
                float distance = Vector3.Distance(note.transform.position, transform.position);
                float score = 0;
                print("nota "+currentNote);
                print("distanciamin "+minDistance);
                print("distancia "+distance);

                minDistance = 600;

                if (distance < minDistance)
                {
                    score = NoteHit(distance);
                }

                holdScore += score;

                if (!firstHoldNoteHit)
                {
                    print("got it");
                    firstHoldNoteHit = true;
                    currentNote = null;
                }
                else
                {
                    if (score == 0)
                    {
                        holdScore = 0;
                    }

                    ResetHoldScore();
                    //GameManager.instance.currentScore += (int)score;
                    //GameManager.instance.UpdateScore();
                }

                GameManager.instance.currentScore += (int)score;
                GameManager.instance.UpdateScore();
            }
        }
    }

    private float NoteHit(float distance)
    {
        float score = Mathf.Lerp(150f, 0f, distance / minDistance);

        print("score " + score);

        if (score <= 150 && score > 120)
        {
            GameManager.instance.NoteHit(iButton, HitType.Perfect);
        }
        else if (score <= 120 && score > 50)
        {
            GameManager.instance.NoteHit(iButton, HitType.Great);
        }
        else if (score <= 50 && score > 0)
        {
            GameManager.instance.NoteHit(iButton, HitType.Good);
        }

        return score;
    }

    public void StartHold()
    {
        if (currentNote != null)
        {
            if (currentNote.GetComponent<NoteBeahviour>().isHold == true)
            {
                isPressed = true;
                CheckHit(currentNote);
            }
        }
    }

    private void ResetHoldScore()
    {
        holdScore = 0;
        firstHoldNoteHit = false;
        secondHoldNoteHit = false;
    }
}
