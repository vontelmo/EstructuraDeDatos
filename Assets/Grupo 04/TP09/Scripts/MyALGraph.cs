using NUnit.Framework.Interfaces;
using System.Collections.Generic;

public class MyALGraph<T>
{
    private Dictionary<T, List<(T, int)>> nodesList = new();

    public Dictionary<T, List<(T, int)>> NodeList => nodesList;

    private IEnumerable<T> vertexs => nodesList.Keys;

    public IEnumerable<T> Vertexs => vertexs;

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

    public void AddEdge(T from, (T, int) edge)
    {
        if(nodesList.ContainsKey(from))
        {
            if (edge.Item1 != null)
            {
                nodesList[from].Add(edge);
            }
            else
            {
                AddVertex(edge.Item1);
                nodesList[from].Add(edge);
            }
        }
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
