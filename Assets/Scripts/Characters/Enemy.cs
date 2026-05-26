using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : Unit
{
    TileNode currentNode;

    void Start()
    {
        // Occupy tiles to make them unreachable
        Vector3Int cell = Vector3Int.RoundToInt(transform.position);
        Debug.Log($"Enemy Pos: {cell}");
        currentNode = GridManager.Instance.GetNode(cell);
        currentNode.occupied = true;
        currentNode.occupant = this;
    }

}