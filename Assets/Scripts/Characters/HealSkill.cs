using UnityEngine;

public class HealSkill : Skill
{
    public int amount = 1;
    public override void Preview(Unit caster, TileNode target)
    {
        PathPreview.Instance.ClearPath();

        if (target == null)
            return;
        if(target.occupant == null)
            return;
        target.occupant.spriteRenderer.color = Color.green;
    }

    public override void Use(
        Unit caster,
        TileNode target)
    {
        if (target?.occupant == null)
            return;

        target.occupant.health += amount;
        Debug.Log("Target Healed");
    }
}
