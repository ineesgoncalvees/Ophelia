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

    private Button button;
    private GameObject currentNote;

    /// <summary>
    /// Método Start()
    /// </summary>
    private void Start()
    {
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
            if ((other.tag == "Notes") && (currentNote.layer != LayerMask.NameToLayer("Hold")))
            {
                // Devolve que o jogador falhou
                currentNote = null;
                GameManager.instance.NoteMiss();
                RemoveClickListener();
            }
            // Ainda tenho que descobrir como desativar o hold quando o jogador
            // falha, da forma que está comentada ele não reconhece se o jogador
            // carregou ou não

            //else if (currentNote.layer == LayerMask.NameToLayer("Hold"))
            //{
            //    currentNote.transform.GetChild(0).gameObject.SetActive(false);
            //}
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
        // Vai buscar a posição da nota
        float notePosition = note.transform.parent.GetComponent<RectTransform>
            ().InverseTransformVector(note.transform.position).x;
        //float notePosition = note.transform.position.x;

        // Vai buscar valor da variável isLeft da classe do movimento das notas
        bool left = note.GetComponentInParent<NotesMovement>().isLeft;

        //if (!(currentNote.layer == LayerMask.NameToLayer("Hold")))
        {
            print(notePosition);
            // Desativa a nota quando acerta
            note.SetActive(false);

            // Se for esquerda
            if (left)
            {
                // E estiver numa certa posição, develove a acuracy ao
                // GameManager para este dar a pontuação adequada
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
            // Se for direita
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
        }
    }
}
