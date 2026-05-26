using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool isMyTurn = false;
    public SpriteRenderer spriteRenderer;
    public bool moving;
    public int baseAttackRange = 1;
    public int rangeAttackRange = 10;
    public int health = 10;
    public List<Skill> skills = new List<Skill>();

    void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public virtual void StartTurn()
    {
        isMyTurn = true;
        spriteRenderer.color = Color.red;
    }

    public virtual void EndTurn()
    {
        isMyTurn = false;
        spriteRenderer.color = Color.white;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log($"Enemy HP: {health}");
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Vector3Int cell = Vector3Int.RoundToInt(transform.position);
        Debug.Log($"Enemy Pos: {cell}");
        TileNode currentNode = GridManager.Instance.GetNode(cell);
        currentNode.occupied = false;
        currentNode.occupant = null;
        Destroy(gameObject);
    }
}