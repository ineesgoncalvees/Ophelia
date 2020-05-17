using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Estrutura que contem array de particulas
/// </summary>
[System.Serializable]
public struct PSHolder
{
    public ParticleSystem[] particleSystems;
}


/// <summary>
/// Classe que vai gerir os combos e a pontuação dos jogadores
/// </summary>
public class GameManager : MonoBehaviour
{
    // Variáveis que guardam o valor da pontuação conforme melhor perfomance
    [HideInInspector]
    public float currentScore;
    // Variável que guarda o combo
    [HideInInspector]
    public int currentCombo;
    [HideInInspector]
    public int maxCombo;

    [SerializeField]
    private bool isMultiplayer;
    [SerializeField]
    private AudioSource musica;
    // Variáveis para mostrar os pontos e combo ao jogador
    [SerializeField]
    private TextMeshProUGUI pointsText;
    [SerializeField]
    private TextMeshProUGUI comboText;
    [SerializeField]
    private TextMeshProUGUI finalPointsText;
    [SerializeField]
    private TextMeshProUGUI maxComboText;
    [SerializeField]
    private GameObject finalPainel;
    [SerializeField]
    private GameObject menuPause;
    // Variável que vai guardar fullcombo
    [SerializeField]
    private TextMeshProUGUI fullCombo;
    [SerializeField]
    private PSHolder[] particleSystems;

    private bool miss;
    private bool isPaused;
    private bool isActive;
    private bool musicStarted;
    private bool ended;
    private bool isDone;

    private Scene activeScene;

    // Intancia da classe GameManager
    public static GameManager instance;

    /// <summary>
    /// Método Start() que inícia valores das variáveis
    /// </summary>
    void Start()
    {
        instance = this;
        currentCombo = 0;
        currentScore = 0;
        miss = false;
        isPaused = false;
        isActive = true;
        ended = false;
        isDone = false;
        musicStarted = false;
        finalPainel.SetActive(false);
        menuPause.SetActive(false);
        fullCombo.GetComponent<TextMeshProUGUI>().enabled = false;
        Time.timeScale = 1;
    }

    private void Update()
    {
#if UNITY_EDITOR
        int nCount = 0;
        if (Input.GetMouseButton(0))
            nCount = 1;
        Vector2 pt = Input.mousePosition;
#else
        int nCount = Input.touchCount;
        Vector2 pt = Vector2.zero;
        if(nCount > 0)
            pt = Input.touches[0].position;
#endif

        MusicPlay();
        MusicEnd();
    }

    void ActivatePS(int button, HitType type)
    {
        ParticleSystem ps = particleSystems[button].particleSystems[(int)type];

        ps.Clear();
        ps.Play();
    }

    /// <summary>
    /// Método que a cada nota que o jogador acerta atualiza a pontuação e o
    /// combo do jogador
    /// </summary>
    public void NoteHit(int iButton, HitType type)
    {
        currentCombo++;
        comboText.text = "" + currentCombo;

        maxCombo = Mathf.Max(currentCombo, maxCombo);

        ActivatePS(iButton, type);
    }

    public void UpdateScore()
    {
        pointsText.text = "" + currentScore;
    }

    /// <summary>
    /// Método chamado quando jogador falha a nota
    /// </summary>
    public void NoteMiss()
    {
        currentCombo = 0;
        comboText.text = "" + currentCombo;
        miss = true;
    }

    /// <summary>
    /// A ser implementado
    /// </summary>
    public IEnumerator FullCombo()
    {
        if (!miss)
        {
            yield return new WaitForSeconds(1);
            fullCombo.GetComponent<TextMeshProUGUI>().enabled = true;
            yield return new WaitForSeconds(2);
            fullCombo.GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }

    /// <summary>
    /// A ser implementado
    /// </summary>
    private void AllPerfect()
    {
        // A ser implementado
    }

    private IEnumerator FinalPainel()
    {
        yield return new WaitForSeconds(4);
        finalPainel.SetActive(true);
        finalPointsText.text = "" + currentScore;
        maxComboText.text = "" + maxCombo;
    }

    private void MusicEnd()
    {
        if (!musica.isPlaying && ended)
        {
            StartCoroutine(FullCombo());
            StartCoroutine(FinalPainel());
            StopCoroutine(FullCombo());
            ended = false;
            isDone = true;
        }
        else if (!musica.isPlaying && musicStarted && !isPaused && !isDone)
        {
            ended = true;
        }
    }

    private void MusicPlay()
    {
        if (musica.isPlaying)
        {
            musicStarted = true;
        }
    }

    public void Restart()
    {
        activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.buildIndex);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Pause()
    {
        if (!isPaused && isActive)
        {
            Time.timeScale = 0;
            isPaused = true;
            isActive = false;
            musica.Pause();
            menuPause.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            isPaused = false;
            isActive = true;
            musica.UnPause();
            menuPause.SetActive(false);
        }
    }
}

