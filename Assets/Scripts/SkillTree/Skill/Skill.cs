using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public abstract SkillType Type { get; }
}

public enum SkillType
{
    Passive,
    Active
}
