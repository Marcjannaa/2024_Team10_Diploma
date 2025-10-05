using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeGameManager : MonoBehaviour
{
    public enum GameResult
    {
        Win, Lose
    }

    public GameResult OnGameFinished()
    {
        return GameResult.Win;
    }

    public void ResetGame()
    {
        throw new NotImplementedException();
    }
}
