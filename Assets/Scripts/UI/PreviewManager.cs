using UnityEngine;

public class PreviewManager : MonoBehaviour
{
    public static PreviewManager Instance;
    Unit currentPreviewedUnit;

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

    public void Clear()
    {
        if (currentPreviewedUnit != null)
        {
            if (currentPreviewedUnit.isMyTurn)
            {
                currentPreviewedUnit.spriteRenderer.color = Color.red;
            }
            else
            {
                currentPreviewedUnit.spriteRenderer.color = Color.white;
            }
        }

        currentPreviewedUnit = null;
    }
}
