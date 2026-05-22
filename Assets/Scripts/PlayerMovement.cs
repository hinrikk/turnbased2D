using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.PlayerSettings;

public class PlayerMovement : MonoBehaviour
{
    bool moving;

    void Update()
    {
        if (moving)
            return;


        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 world = Camera.main.ScreenToWorldPoint( new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));
        world.z = 0;

        Vector3Int cell = Vector3Int.FloorToInt(world);
        Vector3Int startCell = Vector3Int.RoundToInt(transform.position);

        Debug.Log($"Player Start Cell {startCell}");
        Debug.Log($"Player Cell {cell}");

        List<TileNode> path = Pathfinder.Instance.FindPath(startCell, cell);
        List<TileNode> previewPath = Pathfinder.Instance.FindPath(startCell ,cell);

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
}