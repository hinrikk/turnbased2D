using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public enum PlayerMode
{
    Move,
    Attack
}
public enum PlayerAttackType
{
    BaseAttack,
    RangeAttack
}

public class PlayerMovement : Unit{
    bool moving;
    public int baseAttackRange = 1;
    public int rangeAttackRange = 10;
    public PlayerMode mode = PlayerMode.Move;
    public PlayerAttackType attackType = PlayerAttackType.BaseAttack;


    void Update()
    {
        if (!isMyTurn)
            return;

        if (moving)
            return;

        // Get Mouse Input
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 world = Camera.main.ScreenToWorldPoint( new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));
        world.z = 0;
        Vector3Int targetCell = Vector3Int.FloorToInt(world);
        Vector3Int startCell = Vector3Int.RoundToInt(transform.position);


        if(mode == PlayerMode.Move)
        {
            HandleMove(startCell, targetCell);
        }
        if (mode == PlayerMode.Attack)
        {
            PathPreview.Instance.ClearPath();
            HandleAttack(startCell, targetCell);
        }

    }

    void HandleAttack(Vector3Int player, Vector3Int cursor)
    {
        TileNode node = GridManager.Instance.GetNode(cursor);
        if ((node == null) || (node.occupant == null))
        {
            return;
        }

        int currentAttackRange = (attackType == PlayerAttackType.BaseAttack) ? baseAttackRange : rangeAttackRange;
        float rangeToTarget = Mathf.Abs(Vector3.Distance(node.position, player));

        if (rangeToTarget > currentAttackRange)
            
        {
            Debug.Log($"target: {rangeToTarget}, currentAttackRange:{currentAttackRange}");
            return;
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Enemy enemy = node.occupant;
            enemy.TakeDamage(1);
        }
    }

    void HandleMove(Vector3Int start, Vector3Int target)
    {
        TileNode node = GridManager.Instance.GetNode(target);
        List<TileNode> path = Pathfinder.Instance.FindPath(start, target);

        if (path == null) // Invalid node, or no possible path
        {
            return;
        }

        List<TileNode> previewPath = Pathfinder.Instance.FindPath(start, target);
        PathPreview.Instance.ShowPath(previewPath);

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (path != null)
            {
                StartCoroutine(
                    Move(path)
                );
            }
        }
    }


    IEnumerator Move(
        List<TileNode> path)
    {
        moving = true;

        foreach (TileNode node in path)
        {
            Vector3 target = node.position;
   
            while ( Vector3.Distance(  transform.position, target) > 0.05f)
            {
                transform.position = Vector3.MoveTowards( transform.position, target, 3f * Time.deltaTime);
                yield return null;
            }
        }

        moving = false;
    }

    public void ToggleAttackMode()
    {
        if (mode == PlayerMode.Move)
        {
            mode = PlayerMode.Attack;
        }
        else
        {
            mode = PlayerMode.Move;
        }

    }

    public void ChangeAttackType(PlayerAttackType buttonType)
    {
        attackType = buttonType;

    }


}