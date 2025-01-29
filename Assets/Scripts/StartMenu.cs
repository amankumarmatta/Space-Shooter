using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton; 
    private void Start()
    {
        startButton.onClick.AddListener(() => AnimateButton(startButton, StartGame));
        settingsButton.onClick.AddListener(() => AnimateButton(settingsButton, OpenSettings));
        quitButton.onClick.AddListener(() => AnimateButton(quitButton, QuitGame));
    }

    private void AnimateButton(Button button, Action callback)
    {
        button.transform.DOScale(0.8f, 0.1f).OnComplete(() =>
        {
            button.transform.DOScale(1f, 0.1f).OnComplete(() => callback());
        });
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Level Select");
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
