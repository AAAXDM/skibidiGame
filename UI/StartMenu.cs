using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    int gameplayIndex = 1;
    public void StartGame()
    {
        SceneManager.LoadScene(gameplayIndex);
    }
}
