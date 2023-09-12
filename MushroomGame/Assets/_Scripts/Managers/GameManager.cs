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
                ChangeState(GameState.SpawnPlayer);
                break;
            case GameState.SpawnPlayer:
                UnitManager.Instance.SpawnPlayer();
                GameManager.Instance.ChangeState(GameState.SpawnObstacles);
                break;
            case GameState.SpawnObstacles:
                UnitManager.Instance.SpawnObstacles();
                GameManager.Instance.ChangeState(GameState.PlayerTurn);
                break;
            case GameState.PlayerTurn:
                break;
            case GameState.UpdateObstacles:
                UnitManager.Instance.UpdateObstacles();
                GameManager.Instance.ChangeState(GameState.PlayerTurn);
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
    UpdateObstacles = 4
}
