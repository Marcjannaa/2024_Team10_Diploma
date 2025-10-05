using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeGame : MonoBehaviour
{
    public enum GameResult
    {
        Win, Lose
    }

    public GameResult OnGameFinished()
    {
        return GameResult.Win;
    }
}
