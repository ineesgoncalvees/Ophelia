using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int currentScore;
    private int scorePerGood = 100;
    private int scorePerGreat = 120;
    private int scorePerPerfect = 160;

    [SerializeField]
    private TextMeshProUGUI pointsText;
    [SerializeField]
    private TextMeshProUGUI comboText;

    private int currentCombo;

    public static GameManager instance;

    void Start()
    {
        instance = this;
        currentCombo = 0;
        currentScore = 0;
    }

    public void NoteHit()
    {
        pointsText.text = "" + currentScore;
        currentCombo++;
        comboText.text = "" + currentCombo;
    }

    public void GoodHit()
    {
        print("good");
        currentScore += scorePerGood;
        NoteHit();
    }

    public void GreatHit()
    {
        print("great");
        currentScore += scorePerGreat;
        NoteHit();
    }

    public void PerfectHit()
    {
        print("perfect");
        currentScore += scorePerPerfect;
        NoteHit();
    }

    public void NoteMiss()
    {
        //Debug.Log("Missed");
        currentCombo = 0;
        comboText.text = "" + currentCombo;
    }
}

