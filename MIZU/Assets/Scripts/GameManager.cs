using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameStart()
    {
        SceneManager.LoadScene("main Scene");
    }


    public void RetrunTitle()
    {
        SceneManager.LoadScene("titleScene");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  //  開発環境での終了の場合
#else
       Application.Quit();
#endif
    }
}