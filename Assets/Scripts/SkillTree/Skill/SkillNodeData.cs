using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SkillTree/Node")]
public class SkillNodeData : ScriptableObject
{
    public string Name;

    public Sprite Sprite;

    public int Cost;

    public Skill Skill;

    [TextArea()]
    public string Caption;
}
