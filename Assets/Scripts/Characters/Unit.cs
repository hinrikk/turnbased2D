using System;
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
    public event Action OnUnitChanged;

    void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        healthBar = GetComponentInChildren<HealthBar>();
    }

    private void Start()
    {
        this.OnUnitChanged += CombatUI.Instance.RefreshUI;
        this.OccupyNode();
        healthBar.SetHealth(health, maxHealth);
    }

    public virtual void StartTurn()
    {
        isMyTurn = true;
        PreviewManager.Instance.PreviewUnit(this, Color.red);

        if(this is Enemy)
        {
            TurnManager.Instance.EndCurrentTurn();
        }
    }

    public virtual void EndTurn()
    {
        isMyTurn = false;
        PreviewManager.Instance.Clear();
    }

    public void TakeDamage(int amount)
    {
        Debug.Log("Damage!");
        health -= amount;
        healthBar.SetHealth( health, maxHealth);
        if (health <= 0)
        {
            Die();
        }
        OnUnitChanged?.Invoke();
    }

    public void Heal(int amount)
    {
        health += amount;
        health = Mathf.Min(health, maxHealth);
        healthBar.SetHealth(health, maxHealth);
        OnUnitChanged?.Invoke();
    }

    public void OccupyNode()
    {
        Vector3Int cell = Vector3Int.RoundToInt(transform.position);
        currentNode = GridManager.Instance.GetNode(cell);
        currentNode.occupied = false;
        currentNode.occupant = this;
        OnUnitChanged?.Invoke();
    }

    void Die()
    {
        Vector3Int cell = Vector3Int.RoundToInt(transform.position);
        currentNode = GridManager.Instance.GetNode(cell);
        currentNode.occupied = false;
        currentNode.occupant = null;
        TurnManager.Instance.unitQueue.Remove(this);
        OnUnitChanged?.Invoke();
        Destroy(gameObject);
    }
}