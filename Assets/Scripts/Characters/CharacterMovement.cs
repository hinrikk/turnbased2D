using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;
using System;


public class CharacterMovement : Unit {


    public void HandleAttack(Vector3Int player, Vector3Int cursor, PlayerAttackType attackType)
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
            return;
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Unit targetUnit = node.occupant;
            targetUnit.TakeDamage(1);
        }
    }

    public void HandleMove(Vector3Int start, Vector3Int target)
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
                StartCoroutine(Move(node, path));
            }
        }
    }

    public void HandleSkill(Unit caster, Vector3Int cursor, Skill skill)
    {
        TileNode targetNode = GridManager.Instance.GetNode(cursor);
        Debug.Log($"{skill.skillName}");
        skill.Preview(caster, targetNode);

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            skill.Use(caster, targetNode);
        }
    }


    IEnumerator Move(TileNode sourceNode, List<TileNode> path)
    {
        moving = true;
        TileNode currentNode = sourceNode;

        foreach (TileNode node in path)
        {
            // Move towards target
            Vector3 target = node.position;
            while ( Vector3.Distance(  transform.position, target) > 0.05f)
            {
                transform.position = Vector3.MoveTowards( transform.position, target, 3f * Time.deltaTime);
                yield return null;
            }

            currentNode.ClearNode(); // Clear Node that Unit left
            OccupyNode(); // Occupy node the Unit entered
            currentNode = node; // Next iteration step current target node will be the source node
        }

        moving = false;
    }

}