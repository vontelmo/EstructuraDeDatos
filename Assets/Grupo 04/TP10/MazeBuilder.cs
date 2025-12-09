using System;
using System.Collections.Generic;
using System.Diagnostics;

public enum TileType
{
    Wall,       // Pared 
    Floor,      // Suelo 
    Entrance,   // Entrada 
    Exit        // Salida 
}

public static class MazeBuilder
{
    public static Dictionary<(int, int), MyGraphNode> BuildGraph(TileType[,] grid)
    {
        Dictionary<(int, int), MyGraphNode> nodes = new();
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);

        // 1) Crear nodos solo en los tiles Floor
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (grid[x, y] != TileType.Wall)
                {
                    nodes[(x, y)] = new MyGraphNode(x, y);
                }

            }
        }

        // 2) Crear conexiones (edges) con vecinos
        foreach (var kv in nodes)
        {
            int x = kv.Key.Item1;
            int y = kv.Key.Item2;
            MyGraphNode node = kv.Value;

            // Vecinos en 4 direcciones
            TryConnect(nodes, node, x + 1, y); // Derecha
            TryConnect(nodes, node, x - 1, y); // Izquierda
            TryConnect(nodes, node, x, y + 1); // Arriba
            TryConnect(nodes, node, x, y - 1); // Abajo
        }

        return nodes;
    }

    private static void TryConnect(Dictionary<(int, int), MyGraphNode> nodes, MyGraphNode node, int nx, int ny)
    {
        if (nodes.TryGetValue((nx, ny), out var neighbor))
        {
            // Peso del edge = 1
            node.neighbors[neighbor] = 1;
        }
    }

    public static (MyGraphNode entrance, MyGraphNode exit) FindEntranceExit(Dictionary<(int, int), MyGraphNode> nodes, TileType[,] mazeGrid)
    {
        MyGraphNode entrance = null;
        MyGraphNode exit = null;

        int rows = mazeGrid.GetLength(0);
        int cols = mazeGrid.GetLength(1);

        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                if (mazeGrid[x, y] == TileType.Entrance)
                    entrance = nodes[(x, y)];
                if (mazeGrid[x, y] == TileType.Exit)
                    exit = nodes[(x, y)];
            }
        }

        return (entrance, exit);
    }

    public static MyALGraph<MyGraphNode> BuildALGraph(Dictionary<(int, int), MyGraphNode> nodes)
    {
        var graph = new MyALGraph<MyGraphNode>(false);

        // Agregar v�rtices
        foreach (var node in nodes.Values)
            graph.AddVertex(node);

        // Agregar edges
        foreach (var node in nodes.Values)
        {
            foreach (var neighbor in node.neighbors.Keys)
            {
                graph.AddEdge(node, (neighbor, 1));
            }
        }

        return graph;
    }
}
