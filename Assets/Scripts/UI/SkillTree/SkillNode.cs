using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SkillNode : MonoBehaviour
{
    public event UnityAction<SkillNode> Click;

    public SkillNodeData Data => _data;
    public SkillNode[] ConnectedWith => _connectedWith.ToArray();

    public bool IsDiscovered => _isDiscovered;
    private bool _isDiscovered;

    public bool isActive => _button.interactable;

    [SerializeField] private SkillNodeData _data;

    [SerializeField] private List<SkillNode> _connectedWith;

    [SerializeField] private Image _outline;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => { Click?.Invoke(this); });

        foreach (var connection in _connectedWith)
        {
            connection.Connect(this);
        }
    }

    public void SetSctive(bool value)
    {
        _button.interactable = value;
    }

    public void Discover()
    {
        _isDiscovered = true;
        _outline.enabled = true;
    }


    internal void Recover()
    {
        _isDiscovered = false;
        _outline.enabled = false;
    }

    public void Initialize()
    {
        if (_isDiscovered)
        {
            SetSctive(true);
            return;
        }

        foreach (var node in _connectedWith)
        {
            if (node.IsDiscovered)
            {
                SetSctive(true);
                return;
            }
        }

        SetSctive(false);
    }

    #region Editor
#if UNITY_EDITOR
    public event UnityAction<SkillNode> Destroyed;
    private List<SkillNode> _oldConnections;
#endif

#if UNITY_EDITOR

    public void Connect(SkillNode skillNode)
    {
        if (!_connectedWith.Contains(skillNode) & skillNode != this)
        {
            skillNode.Destroyed += Disconnect;
            _connectedWith.Add(skillNode);
            _oldConnections = new List<SkillNode>(_connectedWith);
        }
    }

    public void Disconnect(SkillNode skillNode)
    {
        skillNode.Destroyed -= Disconnect;
        _connectedWith.Remove(skillNode);
        _oldConnections = new List<SkillNode>(_connectedWith);
    }

    private void OnValidate()
    {
        if (_data)
        {
            GetComponent<Image>().sprite = _data.Sprite;
            name = _data.Name + " node";
        }

        if(_oldConnections == null)
        {
            _oldConnections = new List<SkillNode>(_connectedWith);
        }

        var removeList = new List<SkillNode>();

        foreach (var connection in _connectedWith)
        {
            if (!connection || connection == this)
            {
                removeList.Add(connection);
                continue;
            }

            if (!_oldConnections.Contains(connection))
            {
                connection.Connect(this);
                connection.Destroyed += Disconnect;
            }
        }

        foreach (var connection in _oldConnections)
        {
            if (!connection)
            {
                removeList.Add(connection);
                continue;
            }

            if (!_connectedWith.Contains(connection))
            {
                connection.Disconnect(this);
            }
        }

        _connectedWith.RemoveAll(removeList.Contains);
        _oldConnections = new List<SkillNode>(_connectedWith);
    }

    private void OnDrawGizmos()
    {
        foreach (var connection in _connectedWith)
        {
            if (connection)
            {
                Gizmos.DrawLine(transform.position, connection.transform.position);
            }
        }
    }

    private void OnDestroy()
    {
        if (!UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
        {
            if (Time.frameCount != 0 && Time.renderedFrameCount != 0)//not loading scene
            {
                Destroyed?.Invoke(this);
            }
        }
    }
#endif
    #endregion
}
