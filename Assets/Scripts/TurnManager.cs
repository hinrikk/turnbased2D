using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;
    public List<Unit> units =  new();
    public int currentIndex = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        units.AddRange(FindObjectsByType<Unit>());
        units.RemoveAll(unit => unit is Enemy);
        Debug.Log($"Playerunits: {units.Count}");
        units[0].StartTurn();
    }

    public void EndCurrentTurn()
    {
        units[currentIndex].EndTurn();
        currentIndex++;

        if (currentIndex >= units.Count)
        {
            currentIndex = 0;
        }

        units[currentIndex].StartTurn();
    }

    public Unit getCurrentUnit()
    {
        return units[currentIndex];
    }
}