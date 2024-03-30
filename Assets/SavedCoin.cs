using SgLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SavedCoin : MonoBehaviour
{
    [SerializeField] TMP_Text totalCoinCollected;
    [SerializeField] TMP_Text coinCollectedThisRound;
    [SerializeField] TMP_Text coinObtained;
    [SerializeField] TMP_Text coinCount,coincount2;
    [SerializeField] Text totalAllCoins;

    int coinObtainedInt;

    [SerializeField] Button mainMenu;
    [SerializeField] Button slctLevel;



    // Start is called before the first frame update
    void Start()
    {
        totalAllCoins.text = CoinManager.Instance.Coins.ToString();
        mainMenu.onClick.AddListener(MainMenuButton);
        slctLevel.onClick.AddListener(SelectLevelButton);
        //StartCoroutine("ScoreUpdate");
        ScoreUpdate();
    }

    void MainMenuButton()
    {
        SceneManager.LoadScene("Main Menu");
        Utilities.ButtonClickSound();

    }

    void SelectLevelButton()
    {
        SceneManager.LoadScene("Lv Select");
        Utilities.ButtonClickSound();

    }

    // Update is called once per frame
    void Update()
    {



    }

    void ScoreUpdate()
    {
        //yield return new WaitForSeconds(.1f);
        totalCoinCollected.SetText(PlayerPrefs.GetInt("AlreadyCollected").ToString());
        coinCollectedThisRound.SetText(PlayerPrefs.GetInt("coinCollectedThisRound").ToString());
        coinObtained.SetText(PlayerPrefs.GetInt("coinObtained").ToString());
        coinCount.SetText(PlayerPrefs.GetInt("Count").ToString());
        coincount2.SetText(PlayerPrefs.GetInt("Count").ToString());

    }
}
