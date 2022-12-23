using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillPointsViewer : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _tMP_Text;

    private void Awake()
    {
        OnSkillPointsChanged(_player.SkillPoints);
    }

    private void OnEnable()
    {
        _player.SkillPointsChanged += OnSkillPointsChanged;
    }
    
    private void OnDisable()
    {
        _player.SkillPointsChanged -= OnSkillPointsChanged;
    }

    private void OnSkillPointsChanged(int value)
    {
        _tMP_Text.text = "Skill points: " + value;
    }
}
