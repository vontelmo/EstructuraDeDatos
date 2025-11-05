using System;
using System.Collections.Generic;

public class MyALGraph<T> where T : IEquatable<T>
{
    private Dictionary<T, List<(T, int)>> nodesList = new();

    public Dictionary<T, List<(T, int)>> NodeList => nodesList;

    private IEnumerable<T> vertices => nodesList.Keys;

    public IEnumerable<T> Vertices => vertices;

    public bool IsDirectional { get; private set; } = true;

    public MyALGraph(bool isDirectional)
    {
        IsDirectional = isDirectional;
    }

    public void AddVertex(T vertex)
    {
        nodesList.Add(vertex, new List<(T, int)>());
    }

    public void RemoveVertex(T vertex)
    {
        if (!nodesList.ContainsKey(vertex)) return;

        //Antes de todo esto, chequeamos que vertex este en nodesList
        nodesList.Remove(vertex);

        //Pasamos por TODAS las listas de edges
        foreach(List<(T, int)> edges in nodesList.Values)
        {
            //Pasamos por cada edge de cada una de esas listas
            foreach ((T, int) edge in edges)
            {
                //Si ese edge conecta con el vertice a remover, removemos el edge
                if (edge.Item1.Equals(vertex)) edges.Remove(edge);
            }
        }
    }

    private void Internal_AddEdge(T from, T to, int weight)
    {
        if (nodesList.ContainsKey(from))
        {
            if (to != null)
            {
                nodesList[from].Add((to, weight));
            }
            else
            {
                AddVertex(to);
                nodesList[from].Add((to, weight));
            }
        }
    }

    public void AddEdge(T from, (T, int) edge)
    {
        Internal_AddEdge(from, edge.Item1, edge.Item2);

        if (!IsDirectional)
            Internal_AddEdge(edge.Item1, from, edge.Item2);
    }

    public void RemoveEdge(T from, T to)
    {
        if(nodesList.ContainsKey(from))
        {
            foreach ((T, int) edge in nodesList[from])
            {
                if (edge.Item1.Equals(to))
                {
                    nodesList[from].Remove(edge);
                }
            }
        }
    }

    public bool ContainsVertex(T vertex)
    {
        return (nodesList.ContainsKey(vertex));
    }

    public bool ContainsEdge(T from, T to)
    {
        if (nodesList.ContainsKey(from))
        {
            foreach ((T, int) edge in nodesList[from])
            {
                if (edge.Item1.Equals(to))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public int? GetWeight(T from, T to)
    {
        if (nodesList.ContainsKey(from))
        {
            foreach ((T, int) edge in nodesList[from])
            {
                if (edge.Item1.Equals(to))
                {
                    return edge.Item2;
                }
            }
        }
        return null;
    }
}
