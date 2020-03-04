using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Classe que vai gerir os combos e a pontuação dos jogadores
/// </summary>
public class GameManager : MonoBehaviour
{
    // Variáveis que guardam o valor da pontuação conforme melhor perfomance
    private int currentScore;
    private int scorePerGood = 100;
    private int scorePerGreat = 120;
    private int scorePerPerfect = 160;
    private Touch touch;

    // Variáveis para mostrar os pontos e combo ao jogador
    [SerializeField]
    private TextMeshProUGUI pointsText;
    [SerializeField]
    private TextMeshProUGUI comboText;
    [SerializeField]
    private GameObject particules;

    // Variável que guarda o combo
    private int currentCombo;
    // Variável que vai guardar fullcombo
    //[SerializeField]
    //private Text fullCombo;

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
        //fullCombo.GetComponent<Text>().enabled = false;
    }

    private void Update()
    {
        if(touch.phase == TouchPhase.Began)
        {
            particules.SetActive(true);
            particules.transform.position = touch.position;
            
        }
        else if(touch.phase == TouchPhase.Ended)
        {
            particules.SetActive(false);
        }
    }

    /// <summary>
    /// Método que a cada nota que o jogador acerta atualiza a pontuação e o
    /// combo do jogador
    /// </summary>
    public void NoteHit()
    {
        pointsText.text = "" + currentScore;
        currentCombo++;
        comboText.text = "" + currentCombo;
    }

    /// <summary>
    /// Método chamado quando o jogador faz good
    /// </summary>
    public void GoodHit()
    {
        print("good");
        currentScore += scorePerGood;
        NoteHit();
    }

    /// <summary>
    /// Método chamado quando o jogador faz um great
    /// </summary>
    public void GreatHit()
    {
        print("great");
        currentScore += scorePerGreat;
        NoteHit();
    }

    /// <summary>
    /// Método chamado quando jogador faz um perfect
    /// </summary>
    public void PerfectHit()
    {
        print("perfect");
        currentScore += scorePerPerfect;
        NoteHit();
    }

    /// <summary>
    /// Método chamado quando jogador falha a nota
    /// </summary>
    public void NoteMiss()
    {
        //Debug.Log("Missed");
        currentCombo = 0;
        comboText.text = "" + currentCombo;
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

