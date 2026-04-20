using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance;

    [Header("UI References")]
    public Text scoreText;
    public Text collectiblesText;

    [Header("Victory Settings")]
    public int requiredCollectibles = 10;
    public GameObject victoryPanel;
    public Text victoryMessage;

    [Header("Buttons")]
    // 这些按钮已经在游戏中存在，不需要在这里设置
    // public Button restartButton;
    // public Button mainMenuButton;

    private int currentScore = 0;
    private int collectedItems = 0;
    private int totalItems = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        SetupButtons();
    }

    private void Start()
    {
        // 计算总物品数量
        totalItems = FindObjectsOfType<CollectibleItem>().Length;
        UpdateUI();
    }

    private void SetupButtons()
    {
        // 按钮已经在游戏中处理，不需要在这里设置
    }

    public void CollectItem(CollectibleItem item)
    {
        currentScore += item.scoreValue;
        collectedItems++;

        UpdateUI();
        CheckVictory();
    }

    private void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "分数: " + currentScore;
        }

        if (collectiblesText != null)
        {
            collectiblesText.text = "收集: " + collectedItems + " / " + requiredCollectibles;
        }
    }

    private void CheckVictory()
    {
        if (collectedItems >= requiredCollectibles)
        {
            ShowVictory();
        }
    }

    private void ShowVictory()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;

        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }

        if (victoryMessage != null)
        {
            victoryMessage.text = "恭喜！收集了所有小球！\n得分: " + currentScore;
        }

        // 播放胜利音效（如果存在）
        try
        {
            GameFacade.Instance.PlaySound("UI点击");
        }
        catch (System.Exception)
        {
            // 如果音效不存在，不报错
        }
    }

    private void RestartGame()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    private void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
