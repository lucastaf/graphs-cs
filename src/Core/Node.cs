using System.Reflection;

namespace garfos.Core;

public class Node
{
    public readonly int Id;
    public HashSet<Node> ConnectedNodes = new HashSet<Node>();
    public Node(int id){
        this.Id = id; 
    }

    public void Connect(Node dest)
    {
        ConnectedNodes.Add(dest);
    }
}