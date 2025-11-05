using System;
using System.Collections.Generic;

public class MyGraphNode : IEquatable<MyGraphNode> 
{
    public Dictionary<MyGraphNode, float> neighbors {  get; private set; }

    public MyGraphNode()
    {
        neighbors = new Dictionary<MyGraphNode, float>();
    }

    public void AddNeighbor(MyGraphNode neighbor, float cost)
    {
        neighbors.Add(neighbor, cost);
    }

    public bool Equals(MyGraphNode other)
    {
        return this == other;
    }
}
