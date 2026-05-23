using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public static Pathfinder Instance;

    void Awake()
    {
        Instance = this;
    }

    public List<TileNode> FindPath( Vector3Int startPos, Vector3Int targetPos)
    {
        TileNode startNode =GridManager.Instance.GetNode(startPos);
        TileNode targetNode =GridManager.Instance.GetNode(targetPos);
        List<TileNode> openList = new List<TileNode>();
        HashSet<TileNode> closedList = new HashSet<TileNode>();

        openList.Add(startNode);

        while (openList.Count > 0)
        {
            TileNode current =openList[0];

            foreach (TileNode node in openList)
            {
                if (node.fCost < current.fCost)
                    current = node;
            }

            openList.Remove(current);
            closedList.Add(current);

            if (current == targetNode)
            {
                return RetracePath(startNode, targetNode);
            }

            foreach (TileNode neighbor in
                    GetNeighbors(current))
            {
                // Defines when node is unreachable
                if (!neighbor.walkable || neighbor.occupied || closedList.Contains(neighbor))
                    continue;

                int cost = current.gCost + 1;
                if (cost < neighbor.gCost || !openList.Contains(neighbor)){
                    neighbor.gCost = cost;
                    neighbor.hCost = Distance(neighbor, targetNode);
                    neighbor.parent =current;
                    if (!openList.Contains(neighbor))
                        openList.Add(neighbor);
                }
            }
        }
        return null;
    }

    List<TileNode> RetracePath(
        TileNode start,
        TileNode end)
    {
        List<TileNode> path = new();

        TileNode current = end;

        while (current != start)
        {
            path.Add(current);
            current = current.parent;
        }

        path.Reverse();
        return path;
    }

    int Distance(TileNode a,TileNode b)
    {
        return Mathf.Abs(a.position.x - b.position.x)+Mathf.Abs( a.position.y - b.position.y);
    }

    List<TileNode> GetNeighbors(TileNode node)
    {
        List<TileNode> neighbors = new();

        Vector3Int[] dirs =
        {
            Vector3Int.up,
            Vector3Int.down,
            Vector3Int.left,
            Vector3Int.right
        };

        foreach (var dir in dirs)
        {
            TileNode neighbor =GridManager.Instance.GetNode( node.position + dir);

            if (neighbor != null)
                neighbors.Add(neighbor);
        }

        return neighbors;
    }
}