using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool isMyTurn = false;
    SpriteRenderer spriteRenderer;

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