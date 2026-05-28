using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform fill;

    void Awake()
    {
        fill = transform.Find("Fill");
        if (fill == null)
        {
            Debug.LogError("Fill child not found!");
        }
    }

    public void SetHealth( int current, int max)
    {
        float percent = (float)current / max;
        Vector3 scale = fill.localScale;
        scale.x = percent;
        fill.localScale = scale;
    }
}
