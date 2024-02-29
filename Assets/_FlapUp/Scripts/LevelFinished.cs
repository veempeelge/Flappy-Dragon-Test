using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelFinished : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Button nextLevel;
    [SerializeField] Button Home;
    [SerializeField] int currentLevel;
    void Start()
    {
        nextLevel.onClick.AddListener(NextScene);
        Home.onClick.AddListener(HomeScene);
    }

    private void HomeScene()
    {
        SceneManager.LoadScene("Main Menu");
    }

    private void NextScene()
    {
        SceneManager.LoadScene($"Level + {currentLevel}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
