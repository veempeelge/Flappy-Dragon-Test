using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public Button endless, level;
    void Start()
    {
        endless.onClick.AddListener(GoToEndless);
        level.onClick.AddListener(GoToLevel);
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
        SceneManager.LoadScene("Level");
    }
}
