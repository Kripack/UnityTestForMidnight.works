using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject winPanel;
    public int initialZombieCount = 7;
    public int currentZombieCount;
    private List<ZombieBehavior> zombies;

    void Start()
    {
        zombies = new List<ZombieBehavior>(FindObjectsOfType<ZombieBehavior>());
        currentZombieCount = zombies.Count;
    }

    public void ZombieDied()
    {
        currentZombieCount--;
        if (currentZombieCount == 0)
        {
            winPanel.SetActive(true);
            PlayerPrefs.SetInt("Wins", PlayerPrefs.GetInt("Wins", 0) + 1);
            Cursor.lockState = CursorLockMode.None;
            Invoke("LoadMenuScene", 2f);
        }
    }
    void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
}