using System;
using System.Collections.Generic;

public class MyGraphNode : IEquatable<MyGraphNode> 
{
    public Dictionary<MyGraphNode, float> neighbors {  get; private set; }

    public int X { get; }
    public int Y { get; }

    public MyGraphNode(int x, int y)
    {
        X = x;
        Y = y;
        neighbors = new Dictionary<MyGraphNode, float>();
    }

    public void AddNeighbor(MyGraphNode neighbor, float cost)
    {
        if (!neighbors.ContainsKey(neighbor))
        {
            neighbors.Add(neighbor, cost);
        }
    }

    public bool Equals(MyGraphNode other)
    {
        if (other.Equals(null))
            return false;

        return X == other.X && Y == other.Y;
    }

    // esto es necesario???
    public override bool Equals(object obj)
    {
        return Equals(obj as MyGraphNode);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}
