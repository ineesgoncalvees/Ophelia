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
    // Variável que vai guardar fullcombo
    [SerializeField]
    private TextMeshProUGUI fullCombo;
    [SerializeField]
    private Animator ophelia;
    [SerializeField]
    private Animator opheliaGroup;
    [SerializeField]
    private Animator nuvem_Left;
    [SerializeField]
    private Animator nuvem_Right;
    [SerializeField]
    private Animator restartButton;
    [SerializeField]
    private Animator backButton;
    [SerializeField]
    private Animator UI;
    [SerializeField]
    private PSHolder[] particleSystems;
    [SerializeField]
    private ParticleSystem rasto;
    [SerializeField]
    private ParticleSystem stars;

    private bool miss;
    private bool isPaused;
    private bool isActive;
    private bool musicStarted;
    private bool ended;
    private bool isDone;
    private int nStars;

    private int currentComboP1;
    private int currentComboP2;
    private int maxComboP1;
    private int maxComboP2;

    private Scene activeScene;

    [Header("Multiplayer Settings")]
    [SerializeField]
    private bool isMultiplayer;
    [SerializeField]
    private GameObject menuPauseP1;
    [SerializeField]
    private GameObject menuPauseP2;
    [SerializeField]
    private TextMeshProUGUI pointsTextP1;
    [SerializeField]
    private TextMeshProUGUI pointsTextP2;
    [SerializeField]
    private TextMeshProUGUI comboTextP1;
    [SerializeField]
    private TextMeshProUGUI comboTextP2;
    [SerializeField]
    private Animator UI1;
    [SerializeField]
    private Animator UI2;
    [SerializeField]
    private TextMeshProUGUI finalPointsTextP1;
    [SerializeField]
    private TextMeshProUGUI maxComboTextP1;
    [SerializeField]
    private TextMeshProUGUI finalPointsTextP2;
    [SerializeField]
    private TextMeshProUGUI maxComboTextP2;
    [SerializeField]
    private TextMeshProUGUI youWinP1;
    [SerializeField]
    private TextMeshProUGUI youWinP2;
    [SerializeField]
    private TextMeshProUGUI youLoseP1;
    [SerializeField]
    private TextMeshProUGUI youLoseP2;

    public bool IsMultiplayer => isMultiplayer;
    public int P1Score { get; set; }
    public int P2Score { get; set; }

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
        menuPauseP1.SetActive(false);
        menuPauseP2.SetActive(false);
        fullCombo.GetComponent<TextMeshProUGUI>().enabled = false;
        youWinP1.GetComponent<TextMeshProUGUI>().enabled = false;
        youWinP2.GetComponent<TextMeshProUGUI>().enabled = false;
        youLoseP1.GetComponent<TextMeshProUGUI>().enabled = false;
        youLoseP2.GetComponent<TextMeshProUGUI>().enabled = false;
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
        if (isMultiplayer)
        {
            if (iButton <= 3)
            {
                currentComboP1++;

                comboTextP1.text = "" + currentComboP1;
                maxComboP1 = Mathf.Max(currentComboP1, maxComboP1);

                UI1.SetTrigger("hit");

                if (currentComboP1 % 10 == 0)
                {
                    ophelia.SetTrigger("happy");
                }
            }
            else
            {
                currentComboP2++;

                comboTextP2.text = "" + currentComboP2;
                maxComboP2 = Mathf.Max(currentComboP2, maxComboP2);

                UI2.SetTrigger("hit");

                if (currentComboP2 % 10 == 0)
                {
                    ophelia.SetTrigger("happy");
                }
            }
        }
        else
        {
            currentCombo++;
            comboText.text = "" + currentCombo;

            maxCombo = Mathf.Max(currentCombo, maxCombo);

            UI.SetTrigger("hit");

            if (currentCombo % 10 == 0)
            {
                nStars += 10;
                ophelia.SetTrigger("happy");
                stars.Emit(nStars);
            }
        }

        ActivatePS(iButton, type);
    }

    public void UpdateSingleScore()
    {
        pointsText.text = "" + currentScore;
    }

    public void UpdateMultiScore(bool isRight)
    {
        if (isRight) pointsTextP2.text = "" + P2Score;
        else pointsTextP1.text = "" + P1Score;
    }

    /// <summary>
    /// Método chamado quando jogador falha a nota
    /// </summary>
    public void NoteMiss(int iButton, HitType type)
    {
        if (isMultiplayer)
        {
            if (iButton <= 3)
            {
                currentComboP1 = 0;
                comboTextP1.text = "" + currentComboP1;
                miss = true;
                ophelia.SetTrigger("missed");
                UI1.SetTrigger("miss");
                opheliaGroup.SetTrigger("miss");
            }
            else
            {
                currentComboP2 = 0;
                comboTextP2.text = "" + currentComboP2;
                miss = true;
                ophelia.SetTrigger("missed");
                UI2.SetTrigger("miss");
                opheliaGroup.SetTrigger("miss");
            }
        }
        else
        {
            currentCombo = 0;
            comboText.text = "" + currentCombo;
            miss = true;
            ophelia.SetTrigger("missed");
            UI.SetTrigger("miss");
            opheliaGroup.SetTrigger("miss");
        }

        ActivatePS(iButton, type);
    }

    public void NoteEmpty(int iButton, HitType type)
    {
        ActivatePS(iButton, type);
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
        if (IsMultiplayer)
        {
            yield return new WaitForSeconds(4);
            finalPainel.SetActive(true);
            finalPointsTextP1.text = "" + P1Score;
            maxComboTextP1.text = "" + maxComboP1;
            finalPointsTextP2.text = "" + P2Score;
            maxComboTextP2.text = "" + maxComboP2;

            if(P1Score > P2Score)
            {
                youWinP1.GetComponent<TextMeshProUGUI>().enabled = true;
                youLoseP2.GetComponent<TextMeshProUGUI>().enabled = true;
            }
            else
            {
                youWinP2.GetComponent<TextMeshProUGUI>().enabled = true;
                youLoseP1.GetComponent<TextMeshProUGUI>().enabled = true;
            }
        }
        else
        {
            yield return new WaitForSeconds(4);
            rasto.Stop();
            finalPainel.SetActive(true);
            finalPointsText.text = "" + currentScore;
            maxComboText.text = "" + maxCombo;
        }
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
            nuvem_Left.SetTrigger("pause");
            nuvem_Right.SetTrigger("pause");
            restartButton.SetTrigger("pause");
            backButton.SetTrigger("pause");
            isPaused = true;
            isActive = false;
            musica.Pause();
            menuPauseP1.SetActive(true);
            menuPauseP2.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            isPaused = false;
            isActive = true;
            musica.UnPause();
            menuPauseP1.SetActive(false);
            menuPauseP2.SetActive(false);
        }
    }
}

