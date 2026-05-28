using UnityEngine;

public class HealSkill : Skill
{
    public int amount = 1;
    public override void Preview(Unit caster, TileNode target)
    {
        PathPreview.Instance.ClearPath();

        if (target?.occupant == null)
        {
            PreviewManager.Instance.Clear();
            return;
        }
        PreviewManager.Instance.PreviewUnit(target.occupant, Color.green);
    }

    public override void Use(
        Unit caster,
        TileNode target)
    {
        if (target?.occupant == null)
            return;

        target.occupant.Heal(amount);
        Debug.Log("Target Healed");
    }
}
