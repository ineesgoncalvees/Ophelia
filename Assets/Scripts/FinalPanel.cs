using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalPanel : MonoBehaviour
{
    
    [SerializeField]
    private GameObject final;

    [SerializeField]
    private float seconds;

    private void Awake()
    {
        final = GameObject.Find("Final");

    }

    private void Start()
    {
        final.gameObject.SetActive(false);
    }

    private void Update()
    {
        EndPanel();
    }

    private IEnumerator EndPanel() {

        yield return new WaitForSeconds(seconds);

        final.gameObject.SetActive(true);
    }

    public void Return()
    {
        SceneManager.LoadScene("Menu");
    }
}
