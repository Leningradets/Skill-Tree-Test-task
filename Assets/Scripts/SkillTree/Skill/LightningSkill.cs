using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Lightning", menuName = "SkillTree/Skill/Lightning")]
public class LightningSkill : Skill
{
    public override SkillType Type => SkillType.Active;
}
