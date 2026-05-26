using UnityEngine;

public class HealSkill : Skill
{
    public override void Preview(Unit caster, TileNode target)
    {
        PathPreview.Instance.ClearPath();

        if (target == null)
            return;
        if(target.occupant == null)
            return;
        target.occupant.spriteRenderer.color = Color.green;

        Debug.Log("Preview Heal");
    }

    public override void Use(
        Unit caster,
        TileNode target)
    {
        if (target?.occupant == null)
            return;

        Debug.Log("Heal");
    }
}
