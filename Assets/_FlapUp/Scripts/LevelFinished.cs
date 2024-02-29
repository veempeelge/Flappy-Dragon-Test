using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelFinished : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Button selectLevel;
    [SerializeField] Button Home;
    void Start()
    {
        selectLevel.onClick.AddListener(NextScene);
        Home.onClick.AddListener(HomeScene);
    }

    private void HomeScene()
    {
        SceneManager.LoadScene("Main Menu");
    }

    private void NextScene()
    {
        SceneManager.LoadScene("Select Level");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
