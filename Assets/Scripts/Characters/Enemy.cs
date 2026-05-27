using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : Unit
{

    void Start()
    {
        // Occupy tiles to make them unreachable
        this.OccupyNode();
    }

}