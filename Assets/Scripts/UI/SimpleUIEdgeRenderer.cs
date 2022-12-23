using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleUIEdgeRenderer : Graphic
{
    [SerializeField] float _thickness;

    [SerializeField] private Vector3 _from;
    [SerializeField] private Vector3 _to;

    protected override void OnPopulateMesh(VertexHelper vh)
    {          
        vh.Clear();
        
        Vector3 direction = (_to - _from).normalized;
        Vector3 normal = (-Vector3.right * direction.y + Vector3.up * direction.x);
        float uvDistance = Vector3.Distance(_from, _to) / _thickness;

        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;

        vertex.position = _from - transform.position;
        vertex.position -= normal * _thickness * 0.5f;
        vertex.uv0 = new Vector3(0, 0);
        vh.AddVert(vertex);

        vertex.position += normal * _thickness;
        vertex.uv0 = new Vector3(1, 0);
        vh.AddVert(vertex);

        vertex.position = _to - transform.position;
        vertex.position += normal * _thickness * 0.5f;
        vertex.uv0 = new Vector3(1, uvDistance);
        vh.AddVert(vertex);

        vertex.position -= normal * _thickness;
        vertex.uv0 = new Vector3(0, uvDistance);
        vh.AddVert(vertex);

        vh.AddTriangle(0, 1, 2);
        vh.AddTriangle(2, 3, 0);
    }

    public void SetPoints(Vector3 from, Vector3 to)
    {
        _from = from;
        _to = to;

        SetVerticesDirty();
    }
}
