using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fireball", menuName = "SkillTree/Skill/Fireball")]
public class FireballSkill : Skill
{
    public override SkillType Type => SkillType.Active;
}
