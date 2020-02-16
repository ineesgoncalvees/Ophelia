using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private GameObject final;

    // Variável booleana que define se as notas vão para a esquerda ou não
    public bool isLeft;

    // Variáveis para definir se a música e as notas já começaram
    private bool starPlaying;
    private bool hasStarted;

    /// <summary>
    /// Método Start() que inícia os valores das variáveis
    /// </summary>
    void Start()
    {
        velMov = velMov / 60f;
        hasStarted = false;
        starPlaying = false;
        final.gameObject.SetActive(false);
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
            }
        }
    }

    /// <summary>
    /// Método que inícia música
    /// </summary>
    private void PlayMusic()
    {
        // Se a música ainda não está a tocar
        if (!starPlaying)
        {
            // Quando tocam no ecrã ou no espaço

            // startPlaying fica true
            starPlaying = true;

            // Música começa
            music.Play();

        }
    }
    public IEnumerator WaitForSound(AudioClip Sound)
    {
        yield return new WaitUntil(() => music.isPlaying == false);

        final.gameObject.SetActive(true);

    }

    public void Return()
    {
        SceneManager.LoadScene("Menu");
    }
}
