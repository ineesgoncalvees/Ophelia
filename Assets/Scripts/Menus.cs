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
        main.gameObject.SetActive(false);
        story.gameObject.SetActive(false);

        StartCoroutine(StartScreen());

        main.gameObject.SetActive(true);
    }

    private IEnumerator StartScreen()
    {
        start.gameObject.SetActive(true);

        yield return new WaitForSeconds(3);

        start.gameObject.SetActive(false);
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
}
