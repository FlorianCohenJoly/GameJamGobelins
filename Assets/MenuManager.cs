using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button startButton;
    public Button exitButton;


    public void StartGame()
    {
        Debug.Log("Start Game");
        UnityEngine.SceneManagement.SceneManager.LoadScene("ComicScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
