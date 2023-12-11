using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ChangeState(GameState.GenerateGrid);
    }

    public void ChangeState(GameState newState)
    {
        State = newState;
        switch(newState)
        {
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateGrid();
                break;
            case GameState.SpawnPieces:
                UnitManager.Instance.SpawnPieces();
                break;
            case GameState.WhiteTurn: break;
            case GameState.BlackTurn: break;
            case GameState.WhiteWin:
                break;
            case GameState.BlackWin:
                break;

        }
    }
}

public enum GameState
{
    GenerateGrid = 0,
    SpawnPieces = 1,
    WhiteTurn = 2,
    BlackTurn = -2,
    WhiteWin = 3,
    BlackWin = 4// for the Game over screen
}
