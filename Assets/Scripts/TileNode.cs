using UnityEngine;

public class TileNode
{
    public Vector3Int position;

    public bool walkable;
    public bool occupied;

    public int gCost;
    public int hCost;

    public TileNode parent;

    public int fCost
    {
        get { return gCost + hCost; }
    }
}