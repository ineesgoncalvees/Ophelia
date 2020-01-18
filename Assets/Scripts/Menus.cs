using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{

    public GameObject start;
    public GameObject main;
    public GameObject story;
    public GameObject songSelect;

    public GameObject song1;
    public GameObject song2;

    public GameObject coverOne;
    public GameObject coverTwo;

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

    private IEnumerator StartScreen()
    {
        yield return new WaitForSeconds(3);

        start.gameObject.SetActive(false);
        main.gameObject.SetActive(true);
    }

    public void SinglePlayer()
    {
        main.gameObject.SetActive(false);
        songSelect.gameObject.SetActive(true);   
        
        song1.gameObject.SetActive(true);
        coverOne.gameObject.SetActive(false);

        song2.gameObject.SetActive(true);
        coverTwo.gameObject.SetActive(false);
    }

    public void Multiplayer()
    {
        //To be implemented
    }

    public void SongOne() {
        coverOne.gameObject.SetActive(true);
        coverTwo.gameObject.SetActive(false);
    }

    public void SongTwo()
    {
        coverOne.gameObject.SetActive(false);
        coverTwo.gameObject.SetActive(true);
    }

    public void Story()
    {
        main.SetActive(false);
        story.SetActive(true);
    }

    public void StartSongOne()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartSongTwo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }


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
