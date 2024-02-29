using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinScore : MonoBehaviour
{
    [SerializeField] int coinCount;
    GameObject coinTextObject;
    int collecetedCoins;
    TMP_Text tmp;

    public string tagToCount = "Gold";
    bool LevelFinished;

    // Start is called before the first frame update
    void Start()
    {
        LevelFinished = false;
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tagToCount);
        int count = objectsWithTag.Length;
        coinCount = count;
        DontDestroyOnLoad(this);
        // Find the GameObject with the specified name
        

        if (coinTextObject != null)
        {
            // Get the TextMeshPro component attached to the GameObject
            tmp = coinTextObject.GetComponent<TMP_Text>();

            if (tmp != null)
            {
                // You have access to the TMP component here
                // tmp.text = "Your Text";
            }
            else
            {
                Debug.LogWarning("TextMeshPro component not found on GameObject: " + tmp.name);
            }
        }
        else
        {
            Debug.LogWarning("GameObject with the specified name not found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelFinished)
        {
            coinTextObject = GameObject.Find("CoinsCollectedNumber");
            if (coinTextObject != null)
            {
                // Get the TextMeshPro component attached to the GameObject
                tmp = coinTextObject.GetComponent<TMP_Text>();

                if (tmp != null)
                {
                    // You have access to the TMP component here
                    // tmp.text = "Your Text";
                }
                else
                {
                    Debug.LogWarning("TextMeshPro component not found on GameObject: " + tmp.name);
                }
            }
            else
            {
                Debug.LogWarning("GameObject with the specified name not found.");
            }
        }
       
        if (SceneManager.GetActiveScene().name == "Lv Finish")
        {
            tmp.SetText(collecetedCoins.ToString() + "/" + coinCount.ToString());
        }
    }

    public void GetCoins()
    {
        collecetedCoins++;
    }

    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    public void LevelFinish()
    {
        LevelFinished = true;
    }
}
