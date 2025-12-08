using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MazeButtonController : MonoBehaviour
{
    public MazeWalker walker;      // game object
    TilemapToMatrix tilemapToMatrix;
    public TileType[,] mazeGrid;   // matriz del laberinto

    private Dictionary<(int, int), MyGraphNode> nodes = new();
    private MyALGraph<MyGraphNode> graph;
    private MyGraphNode entrance;
    private MyGraphNode exit;


    private List<MyGraphNode> currentPath;
    private bool mapChanged; //TODO: setear en true cada vez que se pinte un tile nuevo

    void Start()
    {
        tilemapToMatrix = GetComponent<TilemapToMatrix>();
        mazeGrid = tilemapToMatrix.ConvertTilemapToMatrix();

        // Crear nodos
        nodes = MazeBuilder.BuildGraph(mazeGrid);

        // Crear grafo
        graph = MazeBuilder.BuildALGraph(nodes);

        // Buscar entrada / salida
        (entrance, exit) = MazeBuilder.FindEntranceExit(nodes, mazeGrid);
    }

    public void OnButtonPressed()
    {
        GetPath(graph, entrance, exit);
        walker.StartWalking(currentPath);

        //Debug Matriz

        for (int y = mazeGrid.GetLength(1) - 1; y >= 0; y--)
        {
            string row = "";
            for (int x = 0; x < mazeGrid.GetLength(0); x++)
            {
                row += mazeGrid[x, y] + " ";
            }
            Debug.Log(row);
        }

    }

    public bool PathIsValid(MyALGraph<MyGraphNode> graph, MyGraphNode entrance, MyGraphNode exit)
    {
        if (mapChanged) GetPath(graph, entrance, exit);
        return currentPath.Count > 0; 
    }

    public void GetPath(MyALGraph<MyGraphNode> graph, MyGraphNode entrance, MyGraphNode exit)
    {
        currentPath = Dijkstra.ExecuteDijkstaPathfinding(entrance, exit, graph);
        mapChanged = false;
    }

    public void SetMapChanged()
    {
        mapChanged = true;
    }
}
