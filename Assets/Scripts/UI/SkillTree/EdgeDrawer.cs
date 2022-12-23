using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeDrawer : MonoBehaviour
{
    [SerializeField] private SimpleUIEdgeRenderer _edgeRendererTemplate;
    [SerializeField] private Transform _container;

    private List<int> _drawnEdges = new List<int>();

    public void DrawEdges(SkillNode[] nodes, int[][] edges)
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

    private void DrawEdge(Vector3 from, Vector3 to)
    {
        var edgeRenderer = Instantiate(_edgeRendererTemplate, _container);
        edgeRenderer.SetPoints(from, to);
    }
}
