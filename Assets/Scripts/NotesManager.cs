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

    /// <summary>
    /// Método chamado quando um collider 2D passa pelos botões que são trigger
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Se o collider tiver a tag de Notes
        if (other.tag == "Notes")
        {
            // Desativa botão quando a nota passa por baixo mas não é a nota
            // do lado pretendido
            if (other.GetComponent<NoteBeahviour>().isRight != isRight) return;

            // O botão fica ativo
            button.interactable = true;
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
            // Desativa botão quando a nota passa por baixo mas não é a nota
            // do lado pretendido
            if (other.GetComponent<NoteBeahviour>().isRight != isRight) return;

            // Se for uma nota que não o hold
            if ((other.tag == "Notes"))
            {
                // Devolve que o jogador falhou
                currentNote = null;
                GameManager.instance.NoteMiss();
                RemoveClickListener();
            }
            else if(other.GetComponent<NoteBeahviour>().isHold == true)
            {
                currentNote = null;
                GameManager.instance.NoteMiss();
                RemoveClickListener();
                currentNote.transform.GetChild(0).gameObject.SetActive(false);
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
    private void CheckHit(GameObject note)
    {
        // Vai buscar valor da variável isLeft da classe do movimento das notas
        bool left = note.GetComponentInParent<NotesMovement>().isLeft;

        //print(notePosition);
        // Desativa a nota quando acerta
        note.SetActive(false);

        // Se for esquerda
        if (left)
        {
            float distance = Vector3.Distance(note.transform.position, transform.position);

            if (distance < minDistance)
            {
                float score = Mathf.Lerp(150f, 0f, distance / minDistance);
                GameManager.instance.currentScore += (int)score;

                print(score);

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
            }
        }
        // Se for direita
        else
        {
            float distance = Vector3.Distance(note.transform.position, transform.position);

            if (distance < minDistance)
            {
                float score = Mathf.Lerp(150f, 100f, distance / minDistance);
                GameManager.instance.currentScore += (int)score;

                if (score <= 150 && score > 100)
                {
                    GameManager.instance.NoteHit(iButton, HitType.Perfect);
                }
                else if (score <= 100 && score > 50)
                {
                    GameManager.instance.NoteHit(iButton, HitType.Great);
                }
                else if (score <= 50 && score > 0)
                {
                    GameManager.instance.NoteHit(iButton, HitType.Good);
                }
            }
        }
    }
}
