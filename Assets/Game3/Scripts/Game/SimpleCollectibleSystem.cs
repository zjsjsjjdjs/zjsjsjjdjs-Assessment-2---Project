using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleCollectibleSystem : MonoBehaviour
{
    [Header("收集设置")]
    public int requiredCollectibles = 10;
    public GameObject victoryPanel;

    [Header("UI设置")]
    public Text scoreText;
    public Text collectiblesText;

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
        Time.timeScale = 0;
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
}
