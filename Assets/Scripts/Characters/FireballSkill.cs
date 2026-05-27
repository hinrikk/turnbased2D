using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class FireballSkill : Skill
{
    public int radius = 1;
    public override void Preview(Unit caster, TileNode target)
    {
        if(target == null)
        {
            return;
        }
        List<TileNode> nodes = GridManager.Instance.GetNodesInRadius(target.position, radius);
        nodes.RemoveAll(node => !node.walkable); // Only consider nodes that are possibly occupied
        if (nodes == null)
        {
            return;
        }
        Debug.Log($"Nodes: {nodes.Count}");
        PreviewManager.Instance.PreviewTiles(nodes);
    }

    public override void Use(
        Unit caster,
        TileNode target)
    {
        Debug.Log("Fireball!");
    }
}
