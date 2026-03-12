using garfos.Core;

namespace garfos.Builder;

public class NonDirectiveGraphBuilder : AbstractGraphBuilder
{
    public NonDirectiveGraphBuilder(List<int> nodes, List<(int, int)> edges) : base(nodes, edges)
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
            else if (edge.Item2 == node.Id)
            {
                Node dest = this._getNodeById(edge.Item1);
                node.Connect(dest);
            }
        }
    }
}