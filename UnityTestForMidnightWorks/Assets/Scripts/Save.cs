using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Save : MonoBehaviour
{
    public Text winText;
    public Text loseText;

    void Start()
    {
        int wins = PlayerPrefs.GetInt("Wins", 0);
        int losses = PlayerPrefs.GetInt("Losses", 0);
        winText.text = "Wins: " + wins.ToString();
        loseText.text = "Losses: " + losses.ToString();
    }
    public void Refresh()
    {
        PlayerPrefs.SetInt("Losses", 0);
        PlayerPrefs.SetInt("Wins", 0);
        PlayerPrefs.Save();
        winText.text = "Wins: 0";
        loseText.text = "Losses: 0";
    }


}
