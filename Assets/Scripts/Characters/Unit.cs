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
    public int maxHealth = 10;
    public List<Skill> skills = new List<Skill>();
    public HealthBar healthBar;
    TileNode currentNode;

    void Awake()
    {

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        healthBar = GetComponentInChildren<HealthBar>();
    }

    private void Start()
    {
        this.OccupyNode();
        healthBar.SetHealth(health, maxHealth);
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
        healthBar.SetHealth( health, maxHealth);
        if (health <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        health += amount;
        health = Mathf.Min(health, maxHealth);
        healthBar.SetHealth(health, maxHealth);
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