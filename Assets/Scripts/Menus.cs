using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{

    public GameObject start;
    public GameObject main;
    public GameObject story;

    private void Awake()
    {
        start = GameObject.Find("Start");
        main = GameObject.Find("Main");
        story = GameObject.Find("Story");
    }

    private void Start()
    {
        start.gameObject.SetActive(true);
        main.gameObject.SetActive(false);
        story.gameObject.SetActive(false);

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Multiplayer()
    {
        //To be implemented
    }

    public void Story()
    {
        main.SetActive(false);
        story.SetActive(true);
    }
    public void Return()
    {
        story.SetActive(false);
        main.SetActive(true);
        
    }
}
