// See https://aka.ms/new-console-template for more information

using garfos.Builder;

Console.WriteLine("Hello, World!");
var graphBuilder = new NonDirectiveGraphBuilder(new List<int> { 1, 2, 3, 4, 5 }, new List<(int, int)> { (3,5), (1, 2), (2, 4), (4, 5), (2, 3), (3, 1) });

var graph = graphBuilder.Build();

var originNode = graph.GetNodeById(3);
var navigationOrden = graph.NavigateFrom(originNode);
foreach (var node in navigationOrden)
{
    Console.WriteLine(node.Id + "\n");
}