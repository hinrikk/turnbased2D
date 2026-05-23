using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour
{
    void Start()
    {
        // Occupy tiles to make them unreachable
        Vector3Int cell = Vector3Int.RoundToInt(transform.position);
        Debug.Log($"Enemy Pos: {cell}");
        GridManager.Instance.GetNode(cell).occupied = true;
    }
}