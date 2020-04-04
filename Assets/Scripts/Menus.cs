using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Classe que gere os menus
/// </summary>
public class Menus : MonoBehaviour
{

    // Variáveis dos menus
    [SerializeField]
    private GameObject start;
    [SerializeField]
    private GameObject main;
    [SerializeField]
    private GameObject story;
    [SerializeField]
    private GameObject songSelect;
    [SerializeField]
    private GameObject multiplayer;

    // Variáveis das músicas
    [SerializeField]
    private GameObject song1;
    [SerializeField]
    private GameObject song2;
    [SerializeField]
    private GameObject song3;
    [SerializeField]
    private GameObject song4;

    // Variáveis para as imagens no menu de selação
    [SerializeField]
    private GameObject coverOne;
    [SerializeField]
    private GameObject coverTwo;
    [SerializeField]
    private GameObject coverThree;
    [SerializeField]
    private GameObject coverFour;

    // Variáveis para as previews das músicas no menu de selação
    [SerializeField]
    private AudioSource songOnePreview;
    [SerializeField]
    private AudioSource songTwoPreview;
    [SerializeField]
    private AudioSource songThreePreview;
    [SerializeField]
    private AudioSource songFourPreview;

    /// <summary>
    /// Método Awake() que corre no princípio do programa e encontra os game
    /// objects
    /// </summary>
    private void Awake()
    {
        start = GameObject.Find("Start");
        main = GameObject.Find("Main");
        story = GameObject.Find("Story");
        songSelect = GameObject.Find("Singleplayer");
        multiplayer = GameObject.Find("Multiplayer");

        song1 = GameObject.Find("Song1");
        song2 = GameObject.Find("Song2");
        song3 = GameObject.Find("Song3");
        song4 = GameObject.Find("Song4");

        coverOne = GameObject.Find("SongOneCover");
        coverTwo = GameObject.Find("SongTwoCover");
        coverThree = GameObject.Find("SongThreeCover");
        coverFour = GameObject.Find("SongFourCover");

    }

    /// <summary>
    /// Método Start() chamado no princípio do programa, mas depois do Awake()
    /// e que inicia as variáveis
    /// </summary>
    private void Start()
    {
        start.gameObject.SetActive(true);
        main.gameObject.SetActive(false);
        story.gameObject.SetActive(false);
        songSelect.gameObject.SetActive(false);
        multiplayer.gameObject.SetActive(false);

        song1.gameObject.SetActive(false);
        song2.gameObject.SetActive(false);
        song3.gameObject.SetActive(false);
        song4.gameObject.SetActive(false);

        coverOne.gameObject.SetActive(false);
        coverTwo.gameObject.SetActive(false);
        coverThree.gameObject.SetActive(false);
        coverFour.gameObject.SetActive(false);

        StartCoroutine(StartScreen());
    }

    /// <summary>
    /// Método do tipo IEnumerator que mostra um ecrã por 3 segundos no início
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
    /// Método que corre quando jogador carrega no botão do single-player e 
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
    /// Método que irá iniciar o menu do multiplayer, ainda a ser implementado
    /// </summary>
    public void Multiplayer()
    {
        main.gameObject.SetActive(false);
        songSelect.gameObject.SetActive(false);
        multiplayer.gameObject.SetActive(true);

        song3.gameObject.SetActive(true);
        coverThree.gameObject.SetActive(false);

        song4.gameObject.SetActive(true);
        coverFour.gameObject.SetActive(false);
    }

    /// <summary>
    /// Método que corre quando o jogador seleciona uma música, mostra a imagem
    /// e o preview da música um, desativando as da outra música
    /// </summary>
    public void SongOne() {
        coverOne.gameObject.SetActive(true);
        coverTwo.gameObject.SetActive(false);
        songOnePreview.Play();
        songTwoPreview.Stop();
    }

    /// <summary>
    /// Método que corre quando o jogador seleciona uma música, mostra a imagem
    /// e o preview da música dois, desativando as da outra música
    /// </summary>
    public void SongTwo()
    {
        coverOne.gameObject.SetActive(false);
        coverTwo.gameObject.SetActive(true);
        songTwoPreview.Play();
        songOnePreview.Stop();
    }

    public void SongThree() {
        coverThree.gameObject.SetActive(true);
        coverFour.gameObject.SetActive(false);
        songThreePreview.Play();
        songFourPreview.Stop();
    }

    /// <summary>
    /// Método que corre quando o jogador seleciona uma música, mostra a imagem
    /// e o preview da música dois, desativando as da outra música
    /// </summary>
    public void SongFour()
    {
        coverThree.gameObject.SetActive(false);
        coverFour.gameObject.SetActive(true);
        songFourPreview.Play();
        songThreePreview.Stop();
    }
    /// <summary>
    /// Método que corre o menu da história
    /// </summary>
    public void Story()
    {
        main.SetActive(false);
        story.SetActive(true);
    }

    /// <summary>
    /// Método que muda de cena para a música um
    /// </summary>
    public void StartSongOne()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Método que muda de cena para a música dois
    /// </summary>
    public void StartSongTwo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    /// <summary>
    /// Método que muda de cena para a música três
    /// </summary>
    public void StartSongThree()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

    /// <summary>
    /// Método que muda de cena para a música quatro
    /// </summary>
    public void StartSongFour()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
    }

    /// <summary>
    /// Método que sai do menu single-player para o de seleção do modo de jogo
    /// </summary>
    public void Return()
    {
        story.SetActive(false);

        songSelect.gameObject.SetActive(false);
        multiplayer.gameObject.SetActive(false);

        song1.gameObject.SetActive(false);
        song2.gameObject.SetActive(false);
        song3.gameObject.SetActive(false);
        song4.gameObject.SetActive(false);

        coverOne.gameObject.SetActive(false);
        coverTwo.gameObject.SetActive(false);

        main.SetActive(true);
    }
}
