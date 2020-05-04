using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Classe que começa a música e o movimento das notas
/// </summary>
public class NotesMovement : MonoBehaviour
{
    // Variável para a velocidade das notas
    [SerializeField]
    private float velMov;
    // Variável para a música
    [SerializeField]
    private AudioSource music;
    [SerializeField]
    private GameObject finalPainel;

    [SerializeField]
    private TextMeshProUGUI pointsText;
    [SerializeField]
    private TextMeshProUGUI comboText;

    // Variável booleana que define se as notas vão para a esquerda ou não
    public bool isLeft;

    // Variáveis para definir se a música e as notas já começaram
    private bool starPlaying;
    private bool hasStarted;
    private bool isPaused;
    private bool isActive;
    private bool firstPlay;

    /// <summary>
    /// Método Start() que inícia os valores das variáveis
    /// </summary>
    void Start()
    {
        velMov = velMov / 60f;
        hasStarted = false;
        starPlaying = false;
        isPaused = false;
        isActive = false;
        firstPlay = true;
        finalPainel.SetActive(false);
    }

    /// <summary>
    /// Método FixedUpdate()
    /// </summary>
    void FixedUpdate()
    {
        MoveNotes();
        PlayMusic();
    }

    /// <summary>
    /// Método que faz o movimento das notas
    /// </summary>
    private void MoveNotes()
    {

        if (!isPaused)
        {
            // Se for esquerda
            if (isLeft)
            {
                // E ainda nao está a mexer
                if (!hasStarted)
                {
                    // hasStarted fica a true
                    hasStarted = true;
                }
                // Se hasStarted for true
                else
                {
                    // Notas movem para a esquerda
                    transform.position -= new Vector3(velMov * Time.fixedDeltaTime, 0f, 0f);
                    isActive = false;
                }
            }
            // Se for direita
            else
            {
                if (!hasStarted)
                {
                    hasStarted = true;
                }
                // Move notas para a direita
                else
                {
                    transform.position -= new Vector3(-velMov * Time.fixedDeltaTime, 0f, 0f);
                    isActive = false;
                }
            }
        }
    }

    private bool isDone = false;

    /// <summary>
    /// Método que inícia música
    /// </summary>
    private void PlayMusic()
    {
        // Se a música ainda não está a tocar
        if (!starPlaying)
        {
            if (!isPaused)
            {
                // startPlaying fica true
                starPlaying = true;

                if (firstPlay)
                {
                    //music.Play();
                    firstPlay = false;
                }
                else
                {
                    music.UnPause();
                    isActive = false;
                    isDone = false;
                }
            }
        }
        else
        {
            if (!music.isPlaying && isDone == false)
            {
                StartCoroutine(GameManager.instance.FullCombo());
                StartCoroutine(FinalPainel());
                StopCoroutine(GameManager.instance.FullCombo());
                isDone = true;
            }
        }
    }

    private IEnumerator FinalPainel()
    {
        yield return new WaitForSeconds(4);
        GameManager.instance.DesableOphelia();
        finalPainel.SetActive(true);
        pointsText.text = "" + GameManager.instance.currentScore;
        comboText.text = "" + GameManager.instance.maxCombo;
    }

    public void Pause()
    {
        isPaused = true;

        if (!isActive)
        {
            // Se for esquerda
            if (isLeft)
            {
                transform.position -= new Vector3(0f, 0f, 0f);
                music.Pause();
                isActive = true;
                isDone = true;
                starPlaying = false;
            }
            // Se for direita
            else
            {
                transform.position -= new Vector3(0f, 0f, 0f);
                music.Pause();
                isActive = true;
                isDone = true;
                starPlaying = false;
            }
        }
        else
        {
            isPaused = false;
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
