using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    public Tilemap groundTilemap;
    public Tilemap obstacleTilemap;

    public int width = 30;
    public int height = 30;

    public TileNode[,] grid;

    private void Awake()
    {
        Instance = this;
        GenerateGrid(); // Has to before Start, otherwhise overrides occupied nodes 
    }

    void GenerateGrid()
    {
        grid = new TileNode[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);

                bool walkable =
                    groundTilemap.HasTile(pos)
                    &&
                    !obstacleTilemap.HasTile(pos);

                TileNode node = new TileNode();

                node.position = pos;
                node.walkable = walkable;

                grid[x, y] = node;
            }
        }
    }

    public TileNode GetNode(Vector3Int pos)
    {
        if (pos.x < 0 || pos.x >= width ||
           pos.y < 0 || pos.y >= height)
            return null;

        return grid[pos.x, pos.y];
    }

    public List<TileNode> GetNodesInRadius(Vector3Int center,int radius)
    {
        List<TileNode> nodes = new();
        for (int x = -radius; x <= radius; x++)
        {
            for (int y = -radius; y <= radius; y++)
            {
                Vector3Int pos = center + new Vector3Int(x, y, 0);
                TileNode node =  GetNode(pos);

                if (node != null)
                {
                    nodes.Add(node);
                }
            }
        }

        return nodes;
    }
}