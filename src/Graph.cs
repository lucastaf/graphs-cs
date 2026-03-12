using garfos.Builder;
using garfos.Core;

public class Graph
{
    private List<Node> _nodes = new List<Node>();
    private List<Node>? _navigatedNodes;

    public Graph(List<Node> nodes)
    {
        this._nodes = nodes;
        Console.WriteLine("Hello, Grafo!");
    }

    public List<Node> NavigateFrom(Node origin)
    {
        _navigatedNodes = new List<Node>();
        var navigationOrden = _navitgateFrom(origin);
        _navigatedNodes = null;
        return navigationOrden;
    }

    private List<Node> _navitgateFrom(Node origin)
    {
        var toNodes = origin.ConnectedNodes;
        List<Node> edgeNavigatedNodes = [origin];
        _navigatedNodes!.Add(origin);
        foreach (var node in toNodes)
        {
            if (_navigatedNodes.Contains(node))
                continue;

            List<Node> childNavigatedNodes = _navitgateFrom(node);
            edgeNavigatedNodes.AddRange(childNavigatedNodes);
        }

        return edgeNavigatedNodes;
    }

    public Node GetNodeById(int id)
    {
        foreach (var node in _nodes)
        {
            if (node.Id == id)
                return node;
        }
        throw new NodeNotFoundException(id);
    }
}