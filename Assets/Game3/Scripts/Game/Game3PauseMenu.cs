using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game3PauseMenu : MonoBehaviour
{
    [Header("UI引用")]
    public GameObject pausePanel;
    public GameObject settingsPanel;

    [Header("按钮引用")]
    public Button resumeButton;
    public Button settingsButton;
    public Button restartButton;
    public Button mainMenuButton;
    public Button backButton;

    [Header("设置项")]
    public Slider volumeSlider;
    public Slider musicSlider;

    private bool isPaused = false;

    void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);
        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        SetupButtons();
        SetupSettings();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void SetupButtons()
    {
        if (resumeButton != null)
            resumeButton.onClick.AddListener(ResumeGame);

        if (settingsButton != null)
            settingsButton.onClick.AddListener(OpenSettings);

        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);

        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(ReturnToMainMenu);

        if (backButton != null)
            backButton.onClick.AddListener(CloseSettings);
    }

    void SetupSettings()
    {
        if (volumeSlider != null)
        {
            volumeSlider.value = 1f;
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }

        if (musicSlider != null)
        {
            musicSlider.value = 1f;
            musicSlider.onValueChanged.AddListener(OnMusicChanged);
        }
    }

    public void TogglePause()
    {
        if (settingsPanel != null && settingsPanel.activeSelf)
        {
            CloseSettings();
            return;
        }

        isPaused = !isPaused;

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;

        if (pausePanel != null)
            pausePanel.SetActive(true);

        GameFacade.Instance.PlaySound("UI点击");
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;

        if (pausePanel != null)
            pausePanel.SetActive(false);
        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        isPaused = false;
        GameFacade.Instance.PlaySound("UI点击");
    }

    void OpenSettings()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);
        if (settingsPanel != null)
            settingsPanel.SetActive(true);

        GameFacade.Instance.PlaySound("UI点击");
    }

    void CloseSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
        if (pausePanel != null)
            pausePanel.SetActive(true);

        GameFacade.Instance.PlaySound("UI点击");
    }

    void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    void OnVolumeChanged(float value)
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in audioSources)
        {
            source.volume = value;
        }
    }

    void OnMusicChanged(float value)
    {
    }
}
