using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public GameObject panelLeaderBoard;

    public void StartGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync(1);
    }

    // Метод для выхода из игры
    public void ExitGame()
    {

        // Если игра запущена в редакторе, то остановить игру
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // Если игра запущена как сборка, то закрыть приложение
        Application.Quit();
        #endif
    }

    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync(0);
    }

    public void OpenLeaderBoard()
    {
       panelLeaderBoard.SetActive(true);
    }

    public void CloseLeaderBoard()
    {
        panelLeaderBoard.SetActive(false);
    }
}
