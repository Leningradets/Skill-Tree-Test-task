using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEdgesDrawer : Graphic
{
    [SerializeField] private float _thickness;
    [SerializeField] private SkillTree _skillTree;

    private VertexHelper _vertexHelper;
    private int _currentEdgrsCount;
    private List<int> _drawnEdges = new List<int>();

    protected override void Start()
    {
        SetVerticesDirty();
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        _vertexHelper = vh;
        _vertexHelper.Clear();

        DrawEdges(_skillTree.Nodes, _skillTree.Edges);

    }

    private void DrawEdges(SkillNode[] nodes, int[][] edges)
    {
        if (nodes != null
            )
        {
            for (int i = 0; i < nodes.Length; i++)
            {
                foreach (var edge in edges[i])
                {
                    if (!_drawnEdges.Contains(edge))
                    {
                        DrawEdge(nodes[i].transform.position, nodes[edge].transform.position);
                    }
                }

                _drawnEdges.Add(i);
            }
        }
    }

    private void DrawEdge(Vector3 from, Vector3 to)
    {
        Vector3 direction = (to - from).normalized;
        Vector3 normal = (-Vector3.right * direction.y + Vector3.up * direction.x);
        float uvDistance = Vector3.Distance(from, to);

        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;

        vertex.position = from - transform.position;
        vertex.position -= normal * _thickness * 0.5f;
        vertex.uv0 = new Vector3(0, 0);
        _vertexHelper.AddVert(vertex);

        vertex.position += normal * _thickness;
        vertex.uv0 = new Vector3(1, 0);
        _vertexHelper.AddVert(vertex);

        vertex.position = to - transform.position;
        vertex.position += normal * _thickness * 0.5f;
        vertex.uv0 = new Vector3(1, uvDistance);
        _vertexHelper.AddVert(vertex);

        vertex.position -= normal * _thickness;
        vertex.uv0 = new Vector3(0, uvDistance);
        _vertexHelper.AddVert(vertex);

        _vertexHelper.AddTriangle(0 + _currentEdgrsCount * 4, 1 + _currentEdgrsCount * 4, 2 + _currentEdgrsCount * 4);
        _vertexHelper.AddTriangle(2 + _currentEdgrsCount * 4, 3 + _currentEdgrsCount * 4, 0 + _currentEdgrsCount * 4);

        _currentEdgrsCount++;
    }
}
