using garfos.Core;

namespace garfos.Builder;

public class DirectiveGraphBuilder : AbstractGraphBuilder
{
    public DirectiveGraphBuilder(List<int> nodes, List<(int, int)> edges) : base(nodes, edges)
    {
    }

    protected override void AddEdgesToNode(Node node, List<(int, int)> edges)
    {
        foreach (var edge in edges)
        {
            if (edge.Item1 == node.Id)
            {
                Node dest = this._getNodeById(edge.Item2);
                node.Connect(dest);
            }
        }
    }
}