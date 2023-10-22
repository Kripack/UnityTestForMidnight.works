using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerTarget : MonoBehaviour
{
    public float hp = 100f;
    public Text hpText;
    public GameObject hit;
    public GameObject losePanel;
    private bool once;

    private void Start()
    {
        
    }
    void Update()
    {
        hpText.text = "HP:" + hp.ToString();
        if (hp <= 0 && !once)
        {
            hp = 0;
            once = true;
            losePanel.SetActive(true);
            PlayerPrefs.SetInt("Losses", PlayerPrefs.GetInt("Losses", 0) + 1);
            Cursor.lockState = CursorLockMode.None;
            Invoke("LoadMenuScene", 2f);
        }
    }
    void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
    public void TakeDamage(float amount)
    {
        Hit();
        hp -= amount;
    }

    void Hit()
    {
        hit.SetActive(true);
        Invoke("OffHit", 0.3f);
    }
    void OffHit()
    {
        hit.SetActive(false);
    }

}
