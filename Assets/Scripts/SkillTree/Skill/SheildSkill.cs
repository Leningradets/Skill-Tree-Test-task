using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sheild", menuName = "SkillTree/Skill/Sheild")]
public class SheildSkill : Skill
{
    public override SkillType Type => SkillType.Passive;
}
