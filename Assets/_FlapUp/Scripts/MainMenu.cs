using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public Button endless, level, credit;
    void Start()
    {
        endless.onClick.AddListener(GoToEndless);
        level.onClick.AddListener(GoToLevel);
        credit.onClick.AddListener(GoToCredit);
    }

    private void GoToCredit()
    {
        SceneManager.LoadScene("Credit");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GoToEndless()
    {
        SceneManager.LoadScene("Endless");
    }

    void GoToLevel()
    {
        SceneManager.LoadScene("Select Level");
    }
}
