using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GamePauseUI : MonoBehaviour
{

    [SerializeField] private Button resumeButton; 
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() => 
        {
            KitchenGameManager.Instance.TogglePauseGame();
        });

        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });

        optionsButton.onClick.AddListener(() =>
        {
            OptionsUI.Instance.Show();
        });
    }

    private void Start()
    {
        KitchenGameManager.Instance.OngamePaused += KitchenManager_OnGamePaused;
        KitchenGameManager.Instance.OngameUnpaused += KitchenManager_OnGameUnpaused;
        Hide();
    }

    private void KitchenManager_OnGamePaused(object sender, EventArgs e)
    {
        Show();
    }

    private void KitchenManager_OnGameUnpaused(object sender, EventArgs e) 
    {
        Hide();
    }

    private void Show() 
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
