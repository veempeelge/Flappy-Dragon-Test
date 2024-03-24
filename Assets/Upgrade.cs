using System;
using System.Collections;
using System.Collections.Generic;
using SgLib;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrade : MonoBehaviour
{

    [SerializeField] Button staminaUpgrade;
    [SerializeField] Button HPButton;

    float currentStamina;
    float upgradedStamina;

    int currentHP;
    int upgradedHP;

    [SerializeField] Text lives;

    // Start is called before the first frame update
    void Start()
     {
        currentStamina = PlayerPrefs.GetFloat("Stamina", 1f);
        currentHP = PlayerPrefs.GetInt("Lives");

        staminaUpgrade.onClick.AddListener(UpgradeStamina);
        HPButton.onClick.AddListener(UpgradeHP);
        lives.text =  currentHP.ToString();
    }

    private void UpgradeStamina()
    {
        if (CoinManager.Instance.Coins >= 20)
        {
            upgradedStamina = currentStamina + .1f;
            PlayerPrefs.SetFloat("Stamina", upgradedStamina);
            CoinManager.Instance.RemoveCoins(20);
            Debug.Log("Stamina = "+ upgradedStamina);

            currentStamina = PlayerPrefs.GetFloat("Stamina");
        }
    }

    private void UpgradeHP()
    {
        if (CoinManager.Instance.Coins >= 100)
        {
            upgradedHP = currentHP + 1;
            PlayerPrefs.SetInt("Lives", upgradedHP);
            CoinManager.Instance.RemoveCoins(100);
            Debug.Log("HP = " + upgradedHP);

            currentHP = PlayerPrefs.GetInt("Lives");
        }
           
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
