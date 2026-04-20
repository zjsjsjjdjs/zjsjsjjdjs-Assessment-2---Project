using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameManager : MonoBehaviour
{
    // 跳转到游戏场景
    public void LoadGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }

    // 退出游戏
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
