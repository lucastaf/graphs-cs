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


    #region Transitive Closure
    public List<(Node, int)> DirectTransitiveClosure(Node origin)
    {
        var distances = origin.DirectTransitiveClosure(new HashSet<Node>(), 0);
        distances.Sort((a, b) => a.Item1.Id.CompareTo(b.Item1.Id));
        return distances;
    }

    public List<(Node, int)> InverseTransitiveClosure(Node origin)
    {
        _navigatedNodes = new HashSet<Node>();
        var distances = _inverseTransitiveClosure(origin, 0);
        _navigatedNodes = null;
        distances.Sort((a, b) => a.Item1.Id.CompareTo(b.Item1.Id));
        return distances;
    }
    private List<(Node, int)> _inverseTransitiveClosure(Node origin, int count)
    {
        var transitiveClosure = new List<(Node, int)> { (origin, count) };
        _navigatedNodes!.Add(origin);

        // Incoming neighbors: nodes that have an edge to 'origin'
        foreach (var candidate in _nodes)
        {
            if (candidate.ConnectedNodes.Contains(origin) && !_navigatedNodes.Contains(candidate))
            {
                var childClosure = _inverseTransitiveClosure(candidate, count + 1);
                transitiveClosure.AddRange(childClosure);
            }
        }

        return transitiveClosure;
    }
    #endregion

    public Graph[] GetAllStronglyConnectedSubGraphs()
    {
        var index = new Dictionary<Node, int>();
        var lowlink = new Dictionary<Node, int>();
        var onStack = new HashSet<Node>();
        var stack = new Stack<Node>();
        var result = new List<Graph>();
        int currentIndex = 0;

        void StrongConnect(Node v)
        {
            index[v] = currentIndex;
            lowlink[v] = currentIndex;
            currentIndex++;
            stack.Push(v);
            onStack.Add(v);

            foreach (var w in v.ConnectedNodes)
            {
                if (!index.ContainsKey(w))
                {
                    StrongConnect(w);
                    lowlink[v] = Math.Min(lowlink[v], lowlink[w]);
                }
                else if (onStack.Contains(w))
                {
                    lowlink[v] = Math.Min(lowlink[v], index[w]);
                }
            }

            if (lowlink[v] == index[v])
            {
                var sccNodes = new List<Node>();
                Node? w = null;
                do
                {
                    w = stack.Pop();
                    onStack.Remove(w);
                    sccNodes.Add(w);
                } while (w != v);

                // create a Graph for this strongly connected component
                result.Add(new Graph(sccNodes));
            }
        }

        foreach (var node in _nodes)
        {
            if (!index.ContainsKey(node))
                StrongConnect(node);
        }

        return result.ToArray();
    }
}

