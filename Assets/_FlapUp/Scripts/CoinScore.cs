using System.Collections;
using System.Collections.Generic;
using SgLib;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CoinScore : MonoBehaviour
{
    [SerializeField] int coinCount;
    int alreadyCollected;
    GameObject coinTextObject;
    int collecetedCoins;
    TMP_Text tmp;

    [SerializeField] Text coinCollectedText;

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
            return;
        }


        Debug.Log(alreadyCollected);
        if (SceneManager.GetActiveScene().name == "Credit")
        {
            DestroyObject();
        }

       
    }

    // Update is called once per frame
    void Update()
    {
        alreadyCollected = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "Coins");

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

        if (coinCollectedText != null)
        {
            coinCollectedText.text = collecetedCoins.ToString();
        }
        else
        {
            return;
        }

        //alreadyCollected = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "Coins");

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
        if (collecetedCoins - alreadyCollected > 0)
        {
            CoinManager.Instance.AddCoins(collecetedCoins - alreadyCollected);
        }
        else
        {
            Debug.Log("No coins collected");
        }

        if (collecetedCoins > alreadyCollected)
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "Coins", collecetedCoins);
        }


        Debug.Log("Coin input = " + collecetedCoins + "-" +  alreadyCollected + "=" + (collecetedCoins-alreadyCollected));
    }
}
