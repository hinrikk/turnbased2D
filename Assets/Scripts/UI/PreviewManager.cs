using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PreviewManager : MonoBehaviour
{
    public static PreviewManager Instance;
    Unit currentPreviewedUnit;
    public List<GameObject> activeHighlights = new List<GameObject>();
    public GameObject tileHighlightPrefab;

    void Awake()
    {
        Instance = this;
    }

    public void PreviewUnit(Unit unit, Color color)
    {
        Clear();

        if (unit == null)
            return;

        currentPreviewedUnit = unit;
        unit.spriteRenderer.color = color;
    }

    public void PreviewTiles(List<TileNode> nodes)
    {
        ClearMarkers();
        Tilemap tilemap = GridManager.Instance.groundTilemap;
        foreach ( TileNode node in nodes)
        {
            GameObject obj =  Instantiate( tileHighlightPrefab, node.position, Quaternion.identity);
            activeHighlights.Add(obj);
        }
    }

    public void Clear()
    {
        if (currentPreviewedUnit != null)
        {
            if (currentPreviewedUnit.isMyTurn)
            {
                currentPreviewedUnit
                    .spriteRenderer.color =
                    Color.red;
            }
            else
            {
                currentPreviewedUnit
                    .spriteRenderer.color =
                    Color.white;
            }
        }

        currentPreviewedUnit = null;

    }

    public void ClearMarkers()
    {
        foreach (GameObject obj in activeHighlights)
        {
            Destroy(obj);
        }
        activeHighlights.Clear();
    }
}
