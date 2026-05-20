using UnityEngine;
using UnityEngine.InputSystem;

public class TileCursor : MonoBehaviour
{
    public Grid grid;
    public Transform highlight;

    void Update()
    {
        Vector2 mousePos =
            Mouse.current.position.ReadValue();

        Vector3 mouseWorld =
            Camera.main.ScreenToWorldPoint(mousePos);

        mouseWorld.z = 0;

        Vector3Int cell =
            grid.WorldToCell(mouseWorld);

        Vector3 cellCenter =
            grid.GetCellCenterWorld(cell);

        highlight.position = cellCenter;
    }
}