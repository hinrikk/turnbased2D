using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;
    public List<Unit> unitQueue = new();
    public int currentIndex = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        unitQueue.AddRange(FindObjectsByType<Unit>());
        Debug.Log($"PlayerunitQueue: {unitQueue.Count}");
        unitQueue[0].StartTurn();
    }

    public void EndCurrentTurn()
    {
        unitQueue[currentIndex].EndTurn();
        currentIndex++;

        if (currentIndex >= unitQueue.Count)
        {
            currentIndex = 0;
        }

        unitQueue[currentIndex].StartTurn();
    }

    public Unit getCurrentUnit()
    {
        Debug.Log($"current index: {currentIndex}, length: {unitQueue.Count}");
        if(unitQueue.Count == 0) {
            return null;
        }
        return unitQueue[currentIndex];
    }
}