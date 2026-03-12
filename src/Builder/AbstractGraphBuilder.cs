using garfos.Core;

namespace garfos.Builder;

public abstract class AbstractGraphBuilder
{
    private List<Node> _nodes = new List<Node>();

    protected AbstractGraphBuilder(List<int> nodes, List<(int, int)> edges)
    {
        foreach (var node in nodes)
        {
            var newNode = new Node(node);
            _nodes.Add(newNode);
        }

        foreach (var node in _nodes)
        {
            AddEdgesToNode(node, edges);
        }
    }

    protected Node _getNodeById(int id)
    {
        foreach (var node in _nodes)
        {
            if (node.Id == id)
                return node;
        }

        throw new NodeNotFoundException(id);
    }

    protected abstract void AddEdgesToNode(Node node, List<(int, int)> edges);
    
    public Graph Build()
    {
        return new Graph(_nodes);
    }
}