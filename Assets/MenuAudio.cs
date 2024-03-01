using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuAudio : MonoBehaviour
{

    public List<string> targetSceneNames = new List<string> { "Level 1", "Level 2", "Level 3", "Endless" };
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (targetSceneNames.Contains(SceneManager.GetActiveScene().name))
        {
            this.gameObject.SetActive(false);
        }
        else if (!targetSceneNames.Contains(SceneManager.GetActiveScene().name))
        { 
            this.gameObject.SetActive(true); 
        }
    }
}
