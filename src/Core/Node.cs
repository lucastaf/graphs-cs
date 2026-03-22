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

    public List<(Node, Node?)> DepthFirstSearch(HashSet<Node> visitedNodes, Node? origin = null)
    {
        var toNodes = ConnectedNodes;
        List<(Node, Node?)> edgeNavigatedNodes = [(this, origin)];
        visitedNodes.Add(this);
        foreach (var node in toNodes)
        {
            if (visitedNodes.Contains(node))
                continue;

            var childNavigatedNodes = node.DepthFirstSearch(visitedNodes, this);
            edgeNavigatedNodes.AddRange(childNavigatedNodes);
        }

        return edgeNavigatedNodes;
    }

    public List<(Node, Node?)> BreadthFirstSearch(HashSet<Node> visitedNodes, bool isStartPoint = false)
    {
        var toNodes = ConnectedNodes;
        List<(Node target, Node? origin)> edgeNavigatedNodes = new();
        if (isStartPoint)
        {
            edgeNavigatedNodes.Add((this, null));
            visitedNodes!.Add(this);
        }

        foreach (var node in toNodes)
        {
            if (!visitedNodes!.Contains(node))
            {
                edgeNavigatedNodes.Add((node, this));
                visitedNodes.Add(node);
            }
        }

        foreach (var node in toNodes)
        {
            if (edgeNavigatedNodes.All(pair => pair.target != node))
                continue;

            var childNavigatedNodes = node.BreadthFirstSearch(visitedNodes, false);
            edgeNavigatedNodes.AddRange(childNavigatedNodes);
        }

        return edgeNavigatedNodes;
    }

    public List<(Node, int)> DirectTransitiveClosure(HashSet<Node> visitedNodes, int count = 0)
    {
        var transitiveClosure = new List<(Node, int)> { (this, count) };
        visitedNodes!.Add(this);

        foreach (var neighbor in ConnectedNodes)
        {
            if (!visitedNodes.Contains(neighbor))
            {
                var childClosure = neighbor.DirectTransitiveClosure(visitedNodes, count + 1);
                transitiveClosure.AddRange(childClosure);
            }
        }

        return transitiveClosure;
    }
}