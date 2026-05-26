using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public int range = 10;
    [SerializeField] 
    public string skillName = "skillName";

    public abstract void Use(Unit caster, TileNode target);
    public abstract void Preview( Unit caster, TileNode target);
}