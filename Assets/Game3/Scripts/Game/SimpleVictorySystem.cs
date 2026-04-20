using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleVictorySystem : MonoBehaviour
{
    [Header("收集设置")]
    public int requiredCollectibles = 10;
    public GameObject victoryPanel;

    [Header("UI设置")]
    public Text scoreText;
    public Text collectiblesText;

    private int currentScore = 0;
    private int collectedItems = 0;
    private bool isVictory = false;

    void Start()
    {
        // 初始化UI
        UpdateUI();

        // 确保胜利面板开始时是隐藏的
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
        }
    }

    void Update()
    {
        // 按ESC键关闭胜利面板
        if (isVictory && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseVictoryPanel();
        }
    }

    public void CollectItem(int scoreValue = 10)
    {
        currentScore += scoreValue;
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
        isVictory = true;
        Cursor.lockState = CursorLockMode.None;

        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }

        // 播放音效
        try
        {
            GameFacade.Instance.PlaySound("UI点击");
        }
        catch {}
    }

    private void CloseVictoryPanel()
    {
        isVictory = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
        }
    }
}
