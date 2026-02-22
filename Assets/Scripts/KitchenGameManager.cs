using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KitchenGameManager : MonoBehaviour
{

    public static KitchenGameManager Instance { get; private set; }
    public event EventHandler OnStateChanged;
    public event EventHandler OngamePaused;
    public event EventHandler OngameUnpaused;

    private enum GameState 
    {
        WaitingToStart,
        CountingToStart,
        GamePlaying,
        GameOver
    }

    private GameState gameState;
    private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 10f;
    private bool isGamePaused = false;

    private void Awake()
    {
        Instance = this;
        gameState = GameState.WaitingToStart;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAcion;
    }

    private void GameInput_OnPauseAcion(object sender, EventArgs e) 
    {
        TogglePauseGame();
    }

    private void Update()
    {
        switch (gameState) 
        {
            case GameState.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0f) 
                {
                    gameState = GameState.CountingToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case GameState.CountingToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f)
                {
                    gameState = GameState.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case GameState.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    gameState = GameState.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case GameState.GameOver:
                break;
        }
    }

    public bool IsGamePlaying() 
    {
        return gameState == GameState.GamePlaying;
    }

    public bool IsCountdownToStartActive() 
    {
        return gameState == GameState.CountingToStart;
    }

    public bool IsGameOver() 
    {
        return gameState == GameState.GameOver;
    }

    public float GetCountdownToStartTimer() 
    {
        return countdownToStartTimer;
    }

    public float GetGamePlayingTimerNormalized() 
    {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }

    public void TogglePauseGame() 
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0f;
            OngamePaused?.Invoke(this, EventArgs.Empty);
        }
        else 
        {
            Time.timeScale = 1f;
            OngameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }
}
