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

    CoinScore coinScore;
    void Start()
    {
        coinScore = FindAnyObjectByType<CoinScore>();
        selectLevel.onClick.AddListener(NextScene);
        Home.onClick.AddListener(HomeScene);
     
    }

    private void HomeScene()
    {
        SceneManager.LoadScene("Main Menu");
        coinScore.DestroyObject();
    }

    private void NextScene()
    {
        SceneManager.LoadScene("Select Level");
        coinScore.DestroyObject();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
