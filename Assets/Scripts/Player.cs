using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public event UnityAction<int> SkillPointsChanged;
    public int SkillPoints => skillPoints;

    private int _skillPoints;

    private int skillPoints 
    {
        get
        {
            return _skillPoints;
        }

        set 
        {
            _skillPoints = value;
            SkillPointsChanged?.Invoke(_skillPoints);
        }
    }

    private List<Skill> _skills = new List<Skill>();

    public void AddPoints(int value)
    {
        skillPoints += value;
    }

    public void RemovePoints(int value)
    {
        skillPoints -= value;
        if(skillPoints < 0)
        {
            skillPoints = 0;
        }
    }

    public bool TrySpentSkillPoints(int value)
    {
        if(skillPoints - value < 0)
        {
            return false;
        }

        skillPoints -= value;
        return true;
    }

    public void AddSkill(Skill skill)
    {
        if (!_skills.Contains(skill))
        {
            _skills.Add(skill);
        }
    }

    public void RemoveSkill(Skill skill)
    {
        if (_skills.Contains(skill))
        {
            _skills.Remove(skill);
        }
    }
}
