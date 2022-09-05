using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Game : MonoBehaviour
{
    #region Singleton:Game

    public static Game Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    [SerializeField]
    TMP_Text[] allCoinsUIText;

    public int Coins;

    private void Start()
    {
        UpdateAllCoinsUIText();
    }

    public void UseCoins(int amount)
    {
        Coins -= amount;
    }
    public void AddCoins(int amount)
    {
        Coins += amount;
    }

    public bool HasEnoughCoins(int amount)
    {
        return (Coins >= amount);
    }

    public void UpdateAllCoinsUIText()
    {
        for (int i = 0; i < allCoinsUIText.Length; i++)
        {
            allCoinsUIText[i].text = Coins.ToString();
        }
    }
}
