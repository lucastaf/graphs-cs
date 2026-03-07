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
            AddEdgesToNode(newNode, edges);
            _nodes.Add(newNode);
        }    
    }
    
    protected abstract void AddEdgesToNode(Node node,  List<(int, int)> edges);
}