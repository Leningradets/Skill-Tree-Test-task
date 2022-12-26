using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    public SkillNode[] Nodes => _nodes;
    public int[][] Edges => _edges;

    [SerializeField] private Player _player;
    [SerializeField] private SkillNode _baseNode;
    [SerializeField] private BinaryMenu _discoveryMenu;
    [SerializeField] private BinaryMenu _recoveryMenu;

    //Граф через массив смежности https://habr.com/ru/company/otus/blog/675730/
    private SkillNode[] _nodes;
    private int[][] _edges;

    private SkillNode _discoveringSkillNode;
    private SkillNode _recoveringSkillNode;

    private void Awake()
    {
        _nodes = GetComponentsInChildren<SkillNode>();
    }

    private void OnEnable()
    {
        foreach (var node in _nodes)
        {
            node.Click += OnSkillNodeClick;
        }
    }
    
    private void OnDisable()
    {
        foreach (var node in _nodes)
        {
            node.Click -= OnSkillNodeClick;
        }
    }

    private void Start()
    {
        _baseNode.SetActive(true);
        _player.AddSkill(_baseNode.Data.Skill);
        _baseNode.Discover();

        ReinitializeNodes();
    }

    private int[] GetConnectionsArray(SkillNode node)
    {
        var connections = new int[node.ConnectedWith.Length];

        for (int i = 0; i < node.ConnectedWith.Length; i++)
        {
            connections[i] = GetNodeIndex(node.ConnectedWith[i]);
        }

        return connections;
    }

    private int GetNodeIndex(SkillNode node)
    {
        for (int i = 0; i < _nodes.Length; i++)
        {
            if(_nodes[i] == node)
            {
                return i;
            }
        }

        throw new ArgumentException($"Nodes array does not containes {node.name}");
    }

    private void OnSkillNodeClick(SkillNode node)
    {
        if(node == _baseNode)
        {
            return;
        }

        if (!node.IsDiscovered)
        {
            TryDiscoverSkill(node);
        }
        else
        {
            TryRecoverSkill(node);
        }
    }

    public bool TryDiscoverSkill(SkillNode skillNode)
    {
        if (_player.SkillPoints >= skillNode.Data.Cost)
        {
            _discoveringSkillNode = skillNode;
            OpenDiscoveryMenu();
            foreach (var node in _nodes)
            {
                node.SetActive(false);
            }
            return true;
        }

        return false;
    }

    private void OpenDiscoveryMenu()
    {
        _discoveryMenu.Open(OnDiscoveryMenuSelection);
    }

    private void OnDiscoveryMenuSelection(bool selection)
    {
        if (selection)
        {
            _player.RemovePoints(_discoveringSkillNode.Data.Cost);
            _player.AddSkill(_discoveringSkillNode.Data.Skill);
            _discoveringSkillNode.Discover();
        }

        ReinitializeNodes();
    }

    public bool TryRecoverSkill(SkillNode skillNode)
    {
        if (IsRecoverable(skillNode))
        {
            _recoveringSkillNode = skillNode;
            OpenRecoveryMenu();
            foreach (var node in _nodes)
            {
                node.SetActive(false);
            }
            return true;
        }

        return false;
    }

    private bool IsRecoverable(SkillNode node)
    {
        if (node.ConnectedWith.Length == 1)
        {
            return true;
        }

        foreach (var connection in node.ConnectedWith)
        {
            if (connection == _baseNode || !connection.IsDiscovered)
            {
                continue;
            }

            var excludeList = new List<SkillNode>();
            excludeList.Add(node);

            if (!TryReachBaseNode(connection, excludeList))
            {
                return false;
            }
        }

        return true;
    }

    private bool TryReachBaseNode(SkillNode node, List<SkillNode> exclude)
    {
        if(node.ConnectedWith.Length == 1)
        {
            return false;
        }

        foreach (var connection in node.ConnectedWith)
        {
            if(!exclude.Contains(connection) && connection.IsDiscovered)
            {
                if (connection == _baseNode)
                {
                    return true;
                }

                exclude.Add(node);

                if (connection.IsDiscovered)
                {
                    return TryReachBaseNode(connection, exclude);
                }
            }
        }

        return false;
    }

    private void OpenRecoveryMenu()
    {
        _recoveryMenu.Open(OnRecoveryMenuSelection);
    }

    private void OnRecoveryMenuSelection(bool selection)
    {
        if (selection)
        {
            _player.AddPoints(_recoveringSkillNode.Data.Cost);
            _player.RemoveSkill(_recoveringSkillNode.Data.Skill);
            _recoveringSkillNode.Recover();
        }

        ReinitializeNodes();
    }

    private void ReinitializeNodes()
    {
        _edges = new int[_nodes.Length][];

        for (int i = 0; i < _nodes.Length; i++)
        {
            _nodes[i].UpdateState();
            _edges[i] = GetConnectionsArray(_nodes[i]);
        }
    }

    public void RecoverAll()
    {
        foreach (var node in _nodes)
        {
            if (node.IsDiscovered)
            {
                if (node != _baseNode)
                {
                    node.Recover();
                    _player.AddPoints(node.Data.Cost);
                }
            }
        }

        ReinitializeNodes();
    }
}
