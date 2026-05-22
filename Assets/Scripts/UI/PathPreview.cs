using System.Collections.Generic;
using UnityEngine;

public class PathPreview : MonoBehaviour
{
    public static PathPreview Instance;
    public GameObject markerPrefab;

    List<GameObject> markers = new List<GameObject>();

    void Awake()
    {
        Instance = this;
    }

    public void ShowPath(List<TileNode> path)
    {
        ClearPath();

        if (path == null)
            return;

        foreach (TileNode node in path)
        {

            GameObject marker = Instantiate(markerPrefab, node.position, Quaternion.identity);
            markers.Add(marker);
        }
    }

    public void ClearPath()
    {
        foreach (GameObject m in markers)
        {
            Destroy(m);
        }
        markers.Clear();
    }
}
