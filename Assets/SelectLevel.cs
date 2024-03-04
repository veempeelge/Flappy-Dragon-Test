using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour
{
    [SerializeField] Button lv1, lv2, lv3, back;



    // Start is called before the first frame update
    void Start()
    {
        lv1.onClick.AddListener(GoLv1);
        lv2.onClick.AddListener(GoLv2);
        lv3.onClick.AddListener(GoLv3);
        back.onClick.AddListener(Back);

        if (PlayerPrefs.GetInt("Level 2") == 1)
        {
            lv2.interactable = true;
        }
        if (PlayerPrefs.GetInt("Level 3") == 1)
        {
            lv3.interactable = true;
        }


    }

    private void GoLv1()
    {
        SceneManager.LoadScene("Level 1");
    }
    private void GoLv2()
    {
        SceneManager.LoadScene("Level 2");
    }
    private void GoLv3()
    {
        SceneManager.LoadScene("Level 3");
    }
    private void Back()
    {
        SceneManager.LoadScene("Main Menu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
