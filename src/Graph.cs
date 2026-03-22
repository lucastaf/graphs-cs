using garfos.Builder;
using garfos.Core;

public class Graph
{
    private List<Node> _nodes = new List<Node>();
    private HashSet<Node>? _navigatedNodes;

    public Graph(List<Node> nodes)
    {
        this._nodes = nodes;
    }

    public List<Node> GetNodes()
    {
        return _nodes;
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


    #region Search
    public List<(Node, Node?)> DepthFirstSearch(Node origin)
    {
        _navigatedNodes = new HashSet<Node>();
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
        _navigatedNodes = new HashSet<Node>();
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

    #endregion

    
    #region Transitive Closure
    public List<(Node, int)> DirectTransitiveClosure(Node origin)
    {
        _navigatedNodes = new HashSet<Node>();
        var distances = _directTransitiveClosure(origin, 0);
        _navigatedNodes = null;
        distances.Sort((a, b) => a.Item1.Id.CompareTo(b.Item1.Id));
        return distances;
    }

    private List<(Node, int)> _directTransitiveClosure(Node origin, int count)
    {
        var transitiveClosure = new List<(Node, int)> { (origin, count) };
        _navigatedNodes!.Add(origin);

        foreach (var neighbor in origin.ConnectedNodes)
        {
            if (!_navigatedNodes.Contains(neighbor))
            {
                var childClosure = _directTransitiveClosure(neighbor, count + 1);
                transitiveClosure.AddRange(childClosure);
            }
        }

        return transitiveClosure;
    }

    public List<(Node, int)> InverteTransitiveClosure(Node origin)
    {
        _navigatedNodes = new HashSet<Node>();
        var distances = _inverteTransitiveClosure(origin, 0);
        _navigatedNodes = null;
        distances.Sort((a, b) => a.Item1.Id.CompareTo(b.Item1.Id));
        return distances;
    }
    private List<(Node, int)> _inverteTransitiveClosure(Node origin, int count)
    {
        var transitiveClosure = new List<(Node, int)> { (origin, count) };
        _navigatedNodes!.Add(origin);

        // Incoming neighbors: nodes that have an edge to 'origin'
        foreach (var candidate in _nodes)
        {
            if (candidate.ConnectedNodes.Contains(origin) && !_navigatedNodes.Contains(candidate))
            {
                var childClosure = _inverteTransitiveClosure(candidate, count + 1);
                transitiveClosure.AddRange(childClosure);
            }
        }

        return transitiveClosure;
    }
    #endregion
}

