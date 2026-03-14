using garfos.Builder;
using garfos.Core;

public class Graph
{
    private List<Node> _nodes = new List<Node>();
    private List<Node>? _navigatedNodes;

    public Graph(List<Node> nodes)
    {
        this._nodes = nodes;
    }

    public List<Node> GetNodes()
    {
        return _nodes;
    }

    public List<(Node, Node?)> DepthFirstSearch(Node origin)
    {
        _navigatedNodes = new List<Node>();
        var navigationOrden = _depthFirstSearch(origin);
        _navigatedNodes = null;
        return navigationOrden;
    }

    private List<(Node, Node?)> _depthFirstSearch(Node destiny, Node? origin = null)
    {
        var toNodes = destiny.ConnectedNodes;
        List<(Node, Node?)> edgeNavigatedNodes = [(destiny, origin)];
        _navigatedNodes!.Add(destiny);
        foreach (var node in toNodes)
        {
            if (_navigatedNodes.Contains(node))
                continue;

            var childNavigatedNodes = _depthFirstSearch(node, destiny);
            edgeNavigatedNodes.AddRange(childNavigatedNodes);
        }

        return edgeNavigatedNodes;
    }

    public List<(Node, Node?)> BreadthFirstSearch(Node origin)
    {
        _navigatedNodes = new List<Node>();
        var navigationOrden = _breadthFirstSearch(origin, true);
        _navigatedNodes = null;
        return navigationOrden;
    }

    private List<(Node, Node?)> _breadthFirstSearch(Node origin, bool isStartPoint = false)
    {
        var toNodes = origin.ConnectedNodes;
        List<(Node target, Node? origin)> edgeNavigatedNodes = new();
        if (isStartPoint)
        {
            edgeNavigatedNodes.Add((origin, null));
            _navigatedNodes!.Add(origin);
        }

        foreach (var node in toNodes)
        {
            if (!_navigatedNodes!.Contains(node))
            {
                edgeNavigatedNodes.Add((node, origin));
                _navigatedNodes.Add(node);
            }
        }

        foreach (var node in toNodes)
        {
            if (edgeNavigatedNodes.All(pair => pair.target != node))
                continue;

            var childNavigatedNodes = _breadthFirstSearch(node);
            edgeNavigatedNodes.AddRange(childNavigatedNodes);
        }

        return edgeNavigatedNodes;
    }

    public List<List<int>> GetAdjacentMatrix()
    {
        var rows = new List<List<int>>();
        foreach (var rowNode in _nodes)
        {
            var row = new List<int>();
            foreach (var cellNode in _nodes)
            {
                row.Add(rowNode.ConnectedNodes.Contains(cellNode) ? 1 : 0);
            }
            rows.Add(row);
        }
        return rows;
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