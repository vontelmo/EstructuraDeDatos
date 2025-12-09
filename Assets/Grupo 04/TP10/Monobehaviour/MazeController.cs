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

    BuildingCreator buildingCreator;

    private List<MyGraphNode> currentPath = new();
    private bool mapChanged = true; //TODO: setear en true cada vez que se pinte un tile nuevo

    void Start()
    {
        mazeGrid = new TileType[0,0];
        buildingCreator = BuildingCreator.GetInstance();
        tilemapToMatrix = GetComponent<TilemapToMatrix>();

        LoadTileMap();

        Debug.Log(currentPath.Count + " : start");
    }

    private void LoadTileMap()
    {
        currentPath.Clear();
        if (mazeGrid.Length > 0)
        {
            tilemapToMatrix.ClearMatrix(mazeGrid);
        }
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
        if (PathIsValid(graph, entrance, exit))
        {
            walker.transform.position = buildingCreator.DefaultMap.GetCellCenterWorld(buildingCreator.DefaultMap.origin + new Vector3Int(currentPath[0].X, currentPath[0].Y, 0));
            walker.StartWalking(currentPath);
            Debug.Log(currentPath.Count + " : button got path");
        }
        else
        {
            Debug.LogWarning("Get a valid Path First");
        }
    }

    private void PrintMatrix()
    {
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
        if (mapChanged)
        {
            LoadTileMap();
            GetPath(graph, entrance, exit);
        }
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
