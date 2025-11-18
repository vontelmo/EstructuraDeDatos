using System.Collections.Generic;
using UnityEngine;

public class MazeButtonController : MonoBehaviour
{
    public MazeWalker walker;      // game object
    public TileType[,] mazeGrid;   // matriz del laberinto

    private Dictionary<(int, int), MyGraphNode> nodes;
    private MyGraphNode entrance;
    private MyGraphNode exit;

    void Start()
    {
        nodes = MazeBuilder.BuildGraph(mazeGrid);
        (entrance, exit) = MazeBuilder.FindEntranceExit(nodes, mazeGrid);
    }

    public void OnButtonPressed()
    {
        List<MyGraphNode> path = MazeBuilder.GetPath(new MyALGraph<MyGraphNode>(false), entrance, exit);
        walker.StartWalking(path);
    }
}
