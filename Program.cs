// See https://aka.ms/new-console-template for more information

using garfos.Builder;
using garfos.parser;

//var graphBuilder = new DirectiveGraphBuilder(new List<int> { 1, 2, 3, 4, 5 }, new List<(int, int)>
// {
//     (1,2),
//     (1,3),

//     (2,4),
//     (2,5),

//     (3,5),
// });

var parser = new GraphCreatorParser();
var graphBuilder = parser.readTerminal();
var graph = graphBuilder.Build();
Console.WriteLine("\nGrafo criado, vamos ao modo de consulta");

var explorer = new GraphExplorerParser(graph);
explorer.ExplorerMenu();