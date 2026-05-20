using UnityEngine;

public class GridVisualizer : MonoBehaviour
{
    public int width = 20;
    public int height = 20;

    void OnDrawGizmos()
    {
        for (int x = 0; x <= width; x++)
        {
            Gizmos.DrawLine(
                new Vector3(x, 0, 0),
                new Vector3(x, height, 0));
        }

        for (int y = 0; y <= height; y++)
        {
            Gizmos.DrawLine(
                new Vector3(0, y, 0),
                new Vector3(width, y, 0));
        }
    }
}