using garfos.Core;

namespace garfos.Builder;

public class NodeNotFoundException : Exception
{
    public int nodeId;

    public NodeNotFoundException(int nodeId)
        : base($"Node {nodeId} not found")
    {
        this.nodeId = nodeId;
    }
}