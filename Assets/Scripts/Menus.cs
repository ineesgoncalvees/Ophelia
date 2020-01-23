using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Classe que gere os menus
/// </summary>
public class Menus : MonoBehaviour
{

    // Variaveis dos menus
    [SerializeField]
    private GameObject start;
    [SerializeField]
    private GameObject main;
    [SerializeField]
    private GameObject story;
    [SerializeField]
    private GameObject songSelect;

    // Variaveis das musicas
    [SerializeField]
    private GameObject song1;
    [SerializeField]
    private GameObject song2;

    // Variaveis para as imagens no menu de selacao
    [SerializeField]
    private GameObject coverOne;
    [SerializeField]
    private GameObject coverTwo;

    // Variaveis para as previesws das musicas no menu de selacao
    [SerializeField]
    private AudioSource songOnePreview;
    [SerializeField]
    private AudioSource songTwoPreview;

    /// <summary>
    /// Metodo Awake() que corre no principo do programa e encontra os game
    /// objects
    /// </summary>
    private void Awake()
    {
        start = GameObject.Find("Start");
        main = GameObject.Find("Main");
        story = GameObject.Find("Story");
        songSelect = GameObject.Find("Song Select");

        song1 = GameObject.Find("Song1");
        song2 = GameObject.Find("Song2");

        coverOne = GameObject.Find("SongOneCover");
        coverTwo = GameObject.Find("SongTwoCover");

    }

    /// <summary>
    /// Metedo Start() chamado no principio do programa mas depois do Awake()
    /// e que inicia as variaveis
    /// </summary>
    private void Start()
    {
        start.gameObject.SetActive(true);
        main.gameObject.SetActive(false);
        story.gameObject.SetActive(false);
        songSelect.gameObject.SetActive(false);

        song1.gameObject.SetActive(false);
        song2.gameObject.SetActive(false);

        coverOne.gameObject.SetActive(false);
        coverTwo.gameObject.SetActive(false);

        StartCoroutine(StartScreen());
    }

    /// <summary>
    /// Metodo do tipo IEnumerator que mostra um ecra por 3 segundos no inicio
    /// do programa e depois do desativa
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartScreen()
    {
        yield return new WaitForSeconds(3);

        start.gameObject.SetActive(false);
        main.gameObject.SetActive(true);
    }

    /// <summary>
    /// Metodo que corre quando jogador carrega no botao do single-player e 
    /// mostra o menu correspondente, desativando o anterior
    /// </summary>
    public void SinglePlayer()
    {
        main.gameObject.SetActive(false);
        songSelect.gameObject.SetActive(true);   
        
        song1.gameObject.SetActive(true);
        coverOne.gameObject.SetActive(false);

        song2.gameObject.SetActive(true);
        coverTwo.gameObject.SetActive(false);
    }

    /// <summary>
    /// Metodo que ira iniciar o menu do multiplay, ainda a ser implementado
    /// </summary>
    public void Multiplayer()
    {
        //To be implemented
    }

    /// <summary>
    /// Metodo que corre quando o jogador seleciona uma musica, mostra a imagem
    /// e o preview da musica um, desativando as da outra musica
    /// </summary>
    public void SongOne() {
        coverOne.gameObject.SetActive(true);
        coverTwo.gameObject.SetActive(false);
        songOnePreview.Play();
        songTwoPreview.Stop();
    }

    /// <summary>
    /// Metodo que corre quando o jogador seleciona uma musica, mostra a imagem
    /// e o preview da musica dois, desativando as da outra musica
    /// </summary>
    public void SongTwo()
    {
        coverOne.gameObject.SetActive(false);
        coverTwo.gameObject.SetActive(true);
        songTwoPreview.Play();
        songOnePreview.Stop();
    }

    /// <summary>
    /// Metodo que corre o menu da historia
    /// </summary>
    public void Story()
    {
        main.SetActive(false);
        story.SetActive(true);
    }

    /// <summary>
    /// Metodo que muda de cena para a musica um
    /// </summary>
    public void StartSongOne()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Metodo que muda de cena para a musica dois
    /// </summary>
    public void StartSongTwo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    /// <summary>
    /// Metodo que sai do menu single-player para o de selecao do modo de jogo
    /// </summary>
    public void Return()
    {
        story.SetActive(false);

        songSelect.gameObject.SetActive(false);

        song1.gameObject.SetActive(false);
        song2.gameObject.SetActive(false);

        coverOne.gameObject.SetActive(false);
        coverTwo.gameObject.SetActive(false);

        main.SetActive(true);
    }
}
