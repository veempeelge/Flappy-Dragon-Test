using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuAudio : MonoBehaviour
{
    public string nameToCount = "MainSong";
    public List<string> targetSceneNames = new List<string> { "Level 1", "Level 2", "Level 3", "Endless" };
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(nameToCount);
        foreach (GameObject go in objectsWithTag)
        {
            // Check if the GameObject has the specified tag
            if (go.CompareTag(nameToCount))
            {
                count++;
                // If more than 1 GameObject with the specified tag is found, destroy the current one
                if (count > 1)
                {
                    Destroy(go);
                }
            }
            // If the GameObject does not have the specified tag, destroy it
            else
            {
                Destroy(go);
            }
        }
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(nameToCount);
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
