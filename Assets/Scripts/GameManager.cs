﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    // Variáveis para mostrar os pontos e combo ao jogador
    [SerializeField]
    private TextMeshProUGUI pointsText;
    [SerializeField]
    private TextMeshProUGUI comboText;
    // Variável que vai guardar fullcombo
    [SerializeField]
    private TextMeshProUGUI fullCombo;
    [SerializeField]
    private PSHolder[] particleSystems;

    [SerializeField]
    private GameObject ophelia;
    [SerializeField]
    private GameObject meio;

    private bool miss;

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
        fullCombo.GetComponent<TextMeshProUGUI>().enabled = false;
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
    }

    void ActivatePS(int button, HitType type)
    {
        ParticleSystem ps = particleSystems[button].particleSystems[(int)type];

        ps.Clear();
        ps.Play();

        print(type);
    }

    /// <summary>
    /// Método que a cada nota que o jogador acerta atualiza a pontuação e o
    /// combo do jogador
    /// </summary>
    public void NoteHit(int iButton, HitType type)
    {
        pointsText.text = "" + currentScore;
        currentCombo++;
        comboText.text = "" + currentCombo;

        maxCombo = Mathf.Max(currentCombo);

        ActivatePS(iButton, type);
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
        if(!miss)
        {
            yield return new WaitForSeconds(1);
            ophelia.SetActive(false);
            meio.SetActive(false);
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
}

