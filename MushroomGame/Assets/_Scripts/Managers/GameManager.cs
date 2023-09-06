using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState GameState;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(GameState.GenerateGrid);
    }

    public void ChangeState(GameState state)
    {
        GameState = state;
        switch (state)
        {
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateGrid();
                break;
            case GameState.SpawnPlayer:
                UnitManager.Instance.SpawnPlayer();
                break;
            case GameState.SpawnObstacles:
                UnitManager.Instance.SpawnObstacle();
                break;
            case GameState.PlayerTurn:
                break;
            case GameState.UpdateAll:
                break;
            default:
                break;
        }
    }
}

public enum GameState {
    GenerateGrid = 0,
    SpawnPlayer = 1,
    SpawnObstacles = 2,
    PlayerTurn = 3,
    UpdateAll = 4
}
