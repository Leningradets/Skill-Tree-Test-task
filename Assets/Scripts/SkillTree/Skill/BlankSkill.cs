using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlankSkill", menuName = "SkillTree/Skill/BlankSkill")]
public class BlankSkill : Skill
{
    public override SkillType Type => _type;

    [SerializeField] private SkillType _type;
}
