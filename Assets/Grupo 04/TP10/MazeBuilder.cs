using System.Collections.Generic;

public enum TileType
{
    Wall,       // Pared (negro)
    Floor,      // Suelo (blanco)
    Entrance,   // Entrada (azul)
    Exit        // Salida (rojo)
}

public static class MazeBuilder
{
    public static Dictionary<(int x, int y), MyGraphNode> BuildGraph(TileType[,] mazeGrid)
    {
        int rows = mazeGrid.GetLength(0);
        int cols = mazeGrid.GetLength(1);

        Dictionary<(int, int), MyGraphNode> nodes = new();

        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                if (mazeGrid[x, y] != TileType.Wall)
                {
                    nodes[(x, y)] = new MyGraphNode(x, y);
                }
            }
        }

        foreach (KeyValuePair<(int, int), MyGraphNode> nodeEntry in nodes)
        {
            int x = nodeEntry.Key.Item1;
            int y = nodeEntry.Key.Item2;
            MyGraphNode node = nodeEntry.Value;

            (int nx, int ny)[] neighbors = new (int, int)[]
            {
                (x - 1, y), // arriba
                (x + 1, y), // abajo
                (x, y - 1), // izquierda
                (x, y + 1)  // derecha
            };

            foreach ((int nx, int ny) in neighbors)
            {
                if (nx >= 0 && nx < rows && ny >= 0 && ny < cols)
                {
                    if (mazeGrid[nx, ny] != TileType.Wall)
                    {
                        node.AddNeighbor(nodes[(nx, ny)], 1f);
                    }
                }
            }
        }

        return nodes;
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
                if (mazeGrid[x, y] == TileType.Entrance && nodes.ContainsKey((x, y)))
                    entrance = nodes[(x, y)];
                if (mazeGrid[x, y] == TileType.Exit && nodes.ContainsKey((x, y)))
                    exit = nodes[(x, y)];
            }
        }

        return (entrance, exit);
    }
}
