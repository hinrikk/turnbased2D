using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool isMyTurn = false;
    SpriteRenderer spriteRenderer;
    public bool moving;
    public int baseAttackRange = 1;
    public int rangeAttackRange = 10;

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
}