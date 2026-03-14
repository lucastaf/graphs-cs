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

    public List<Node> DepthFirstSearch(Node origin)
    {
        _navigatedNodes = new List<Node>();
        var navigationOrden = _depthFirstSearch(origin);
        _navigatedNodes = null;
        return navigationOrden;
    }

    private List<Node> _depthFirstSearch(Node origin)
    {
        var toNodes = origin.ConnectedNodes;
        List<Node> edgeNavigatedNodes = [origin];
        _navigatedNodes!.Add(origin);
        foreach (var node in toNodes)
        {
            if (_navigatedNodes.Contains(node))
                continue;

            List<Node> childNavigatedNodes = _depthFirstSearch(node);
            edgeNavigatedNodes.AddRange(childNavigatedNodes);
        }

        return edgeNavigatedNodes;
    }

    public List<Node> BreadthFirstSearch(Node origin)
    {
        _navigatedNodes = new List<Node>();
        var navigationOrden = _breadthFirstSearch(origin, true);
        _navigatedNodes = null;
        return navigationOrden;
    }

    private List<Node> _breadthFirstSearch(Node origin, bool isStartPoint = false)
    {
        Console.WriteLine($"Origin: {origin}");
        var toNodes = origin.ConnectedNodes;
        List<Node> edgeNavigatedNodes = new List<Node>();
        if (isStartPoint)
        {
            edgeNavigatedNodes.Add(origin);
            _navigatedNodes!.Add(origin);
        }

        foreach (var node in toNodes)
        {
            if (!_navigatedNodes.Contains(node))
            {
                edgeNavigatedNodes.Add(node);
                _navigatedNodes.Add(node);
            }
        }

        foreach (var node in toNodes)
        {
            if (!edgeNavigatedNodes.Contains(node))
                continue;

            List<Node> childNavigatedNodes = _breadthFirstSearch(node);
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