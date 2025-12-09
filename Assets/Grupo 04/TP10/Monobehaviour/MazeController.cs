using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class MazeButtonController : MonoBehaviour
{
    public MazeWalker walker;
    public TMP_Text text;
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
        mazeGrid = new TileType[0, 0];
        buildingCreator = BuildingCreator.GetInstance();
        tilemapToMatrix = GetComponent<TilemapToMatrix>();

        LoadTileMap();

        Debug.Log(currentPath.Count + " : start");
    }

    private void LoadTileMap()
    {
        currentPath.Clear();
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
        if (PathIsValid() && tilemapToMatrix.IsPathValid)
        {
            walker.transform.position = buildingCreator.DefaultMap.GetCellCenterWorld(buildingCreator.DefaultMap.origin + new Vector3Int(currentPath[0].X, currentPath[0].Y, 0));
            walker.StartWalking(currentPath);
            Debug.Log(currentPath.Count + " : button got path");
            text.text = "Path is valid";
        }
        else
        {
            text.text = tilemapToMatrix.Message;
            Debug.LogWarning("Get a valid Path First");
        }
    }

    public void PrintMatrix()
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

    public bool PathIsValid()
    {
        if (mapChanged)
        {
            LoadTileMap();
            GetPath();
        }
        return currentPath != null && currentPath.Count > 0;
    }

    public void GetPath()
    {
        currentPath = Dijkstra.ExecuteDijkstaPathfinding(entrance, exit, graph);
        mapChanged = false;
    }

    public void SetMapChanged()
    {
        mapChanged = true;
    }
}
