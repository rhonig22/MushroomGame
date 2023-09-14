using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public int CurrentTruffleCount { get; private set; } = 0;
    public int TruffleGoal { get; private set; } = 4;

    private void Awake()
    {
        Instance = this;
    }

    public void AddCollectible(BaseCollectible collectible)
    {
        if (collectible is Truffle) {
            CurrentTruffleCount++;
        }
    }
}
