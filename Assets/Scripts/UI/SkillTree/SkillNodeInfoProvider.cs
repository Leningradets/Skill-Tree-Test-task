using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SkillNode))]
public class SkillNodeInfoProvider : MonoBehaviour
{
    private SkillNodeInfoViewer _viewer;
    private SkillNodeData _nodeData;

    private void Awake()
    {
        _viewer = FindObjectOfType<SkillNodeInfoViewer>();
        _nodeData = GetComponent<SkillNode>().Data;
    }

    public void ActivateViewer()
    {
        _viewer.SetSkillNodeData(_nodeData);
        _viewer.gameObject.SetActive(true);
    }

    public void DeactivateViewer()
    {
        _viewer.gameObject.SetActive(false);
    }
}
