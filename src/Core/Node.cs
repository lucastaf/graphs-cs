namespace garfos.Core;

public class Node
{
    public readonly int Id;
    public HashSet<Node> ConnectedNodes;
    public Node(int id){
        this.Id = id;  
    }

    public void Connect(Node origin)
    {
        ConnectedNodes.Add(origin);
    }
}