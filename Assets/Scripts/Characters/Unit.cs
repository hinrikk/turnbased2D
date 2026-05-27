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
    TileNode currentNode;

    void Awake()
    {

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        this.OccupyNode();
    }

    public virtual void StartTurn()
    {
        isMyTurn = true;
        PreviewManager.Instance.PreviewUnit(this, Color.red);
    }

    public virtual void EndTurn()
    {
        isMyTurn = false;
        PreviewManager.Instance.Clear();
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

    public void OccupyNode()
    {
        Vector3Int cell = Vector3Int.RoundToInt(transform.position);
        currentNode = GridManager.Instance.GetNode(cell);
        currentNode.occupied = false;
        currentNode.occupant = this;
    }

    void Die()
    {
        Vector3Int cell = Vector3Int.RoundToInt(transform.position);
        currentNode = GridManager.Instance.GetNode(cell);
        currentNode.occupied = false;
        currentNode.occupant = null;
        Destroy(gameObject);
    }
}