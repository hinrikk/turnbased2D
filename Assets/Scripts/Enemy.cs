using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour
{
    public int health = 10;
    TileNode currentNode;

    void Start()
    {
        // Occupy tiles to make them unreachable
        Vector3Int cell = Vector3Int.RoundToInt(transform.position);
        Debug.Log($"Enemy Pos: {cell}");
        currentNode = GridManager.Instance.GetNode(cell);
        currentNode.occupied = true;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        Debug.Log(
            $"Enemy HP: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        currentNode.occupied = false;
        Destroy(gameObject);
    }
}