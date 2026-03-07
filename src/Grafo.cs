using garfos.Core;

class Grafo
{
    private List<Node> _nodes = new List<Node>();
    private List<Node> _navigatedNodes;

    public Grafo(List<Node> nodes, List<(int, int)> edges)
    {
        foreach (var node in nodes)
        {
            _nodes.Add(new Node(node));
        }

        foreach (var edge in edges)
        {
            
        }
        this._nodes = nodes;
        this._edges = edges;
        Console.WriteLine("Hello, Grafo!");
    }

    private bool isNodeConnectedTo(int source, int target)
    {
        foreach (var edge in _edges)
        {
            if (edge.source == source && edge.target == target)
            {
                return true;
            }
        }

        return false;
    }

    public List<List<int>> GetMatrizDirigido()
    {
        List<List<int>> matrizDirigido = new List<List<int>>();

        foreach (int i in _nodes)
        {
            List<int> row = new List<int>();
            foreach (int j in _nodes)
            {
                if (isNodeConnectedTo(i, j))
                {
                    row.Add(1);
                }
                else
                {
                    row.Add(0);
                }
            }
            matrizDirigido.Add(row);
        }

        return matrizDirigido;
    }

    public List<int> NavigateFrom(int origin)
    {
        _navigatedNodes = new List<int>();
        var navigationOrden = _navitgateFrom(origin);
        _navigatedNodes = null;
        return navigationOrden;
    }

    private List<int> _navitgateFrom(int origin)
    {
        var toNodes = _edges.Where(item => item.source == origin).ToList();
        List<int> edgeNavigatedNodes = new List<int>();
        edgeNavigatedNodes.Add(origin);
        _navigatedNodes.Add(origin);
        foreach (var node in toNodes)
        {
            if (_navigatedNodes.Contains(node.target))
                continue;


            List<int> childNavigatedNodes = _navitgateFrom(node.target);
            edgeNavigatedNodes.AddRange(childNavigatedNodes);
        }

        return edgeNavigatedNodes;
    }

}