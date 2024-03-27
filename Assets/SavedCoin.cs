using SgLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SavedCoin : MonoBehaviour
{
    [SerializeField] TMP_Text totalCoinCollected;
    [SerializeField] TMP_Text coinCollectedThisRound;
    [SerializeField] TMP_Text coinObtained;
    [SerializeField] TMP_Text coinCount,coincount2;

    int coinObtainedInt;


    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine("ScoreUpdate");
        ScoreUpdate();
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
