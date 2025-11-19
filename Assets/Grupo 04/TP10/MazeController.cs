using System.Collections.Generic;
using UnityEngine;

public class MazeButtonController : MonoBehaviour
{
    public MazeWalker walker;      // game object
    public TileType[,] mazeGrid;   // matriz del laberinto

    private Dictionary<(int, int), MyGraphNode> nodes;
    private MyGraphNode entrance;
    private MyGraphNode exit;

    private List<MyGraphNode> currentPath;
    private bool mapChanged; //TODO: setear en true cada vez que se pinte un tile nuevo

    void Start()
    {
        nodes = MazeBuilder.BuildGraph(mazeGrid);
        (entrance, exit) = MazeBuilder.FindEntranceExit(nodes, mazeGrid);
    }

    public void OnButtonPressed()
    {
        GetPath(new MyALGraph<MyGraphNode>(false), entrance, exit);
        walker.StartWalking(currentPath);
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
