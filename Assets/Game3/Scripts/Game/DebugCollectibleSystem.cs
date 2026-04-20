using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugCollectibleSystem : MonoBehaviour
{
    [Header("收集设置")]
    public int requiredCollectibles = 10;
    public GameObject victoryPanel;

    [Header("UI设置")]
    public Text scoreText;
    public Text collectiblesText;
    public Text debugText;

    private int currentScore = 0;
    private int collectedItems = 0;

    void Start()
    {
        // 初始化UI
        UpdateUI();

        // 确保胜利面板开始时是隐藏的
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
        }

        // 显示调试信息
        ShowDebugInfo("收集系统已初始化");
    }

    public void CollectItem(int scoreValue = 10)
    {
        currentScore += scoreValue;
        collectedItems++;

        UpdateUI();
        ShowDebugInfo($"收集了一个小球！当前: {collectedItems}/{requiredCollectibles}");
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
            ShowDebugInfo("胜利！");
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
            ShowDebugInfo("胜利面板已显示");
        }
        else
        {
            ShowDebugInfo("错误：胜利面板未设置");
        }
    }

    private void ShowDebugInfo(string message)
    {
        if (debugText != null)
        {
            debugText.text += message + "\n";
        }
        Debug.Log("[收集系统] " + message);
    }

    // 测试按钮用
    public void TestCollectItem()
    {
        CollectItem();
    }

    public void TestVictory()
    {
        collectedItems = requiredCollectibles;
        UpdateUI();
        ShowVictory();
    }
}
