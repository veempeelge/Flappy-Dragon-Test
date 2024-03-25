using SgLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavedCoin : MonoBehaviour
{
    [SerializeField] Text totalCoin;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        totalCoin.text = CoinManager.Instance.Coins.ToString();
    }
}
