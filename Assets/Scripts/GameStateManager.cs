using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private enum GameState
    {
        Playing,
        Paused,
        GameOver,
        Cutscene
    }
    
    public static GameStateManager Instance;

    private GameState currentState { get; set; } = GameState.Playing;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void SetState(GameState newState)
    {
        currentState = newState;

        Time.timeScale = newState switch
        {
            GameState.Paused => 0f,
            GameState.Playing => 1f,
            GameState.GameOver => 0f,
            _ => Time.timeScale
        };
    }
    public void TogglePause()
    {
        switch (currentState)
        {
            case GameState.Playing:
                SetState(GameState.Paused);
                break;
            case GameState.Paused:
                SetState(GameState.Playing);
                break;
            case GameState.GameOver:
                break;
            case GameState.Cutscene:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    public bool IsPaused => currentState == GameState.Paused;
}