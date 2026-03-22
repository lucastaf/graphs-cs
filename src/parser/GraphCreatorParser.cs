using garfos.Builder;
using garfos.Core;

namespace garfos.parser;

public class GraphCreatorParser
{
    List<int> nodes = new List<int>();
    List<(int, int)> edges = new List<(int, int)>();
    bool isDirective;

    public AbstractGraphBuilder readTerminal()
    {
        while (true)
        {
            int selection = TuiTools.MenuSelect(new List<string> { "Inserir nós", "Inserir conexões", "Visualizar grafo", "Confirmar criação" }, "Selecione a ação desejada");

            switch (selection)
            {
                case 0:
                    getNodes();
                    break;
                case 1:
                    getEdges();
                    break;
                case 2:
                    printCurrentGraph();
                    break;
                case 3:
                    int isDirective = TuiTools.MenuSelect(new List<string> { "Diretivo", "Não diretivo" }, "Selecione o modo de direção");
                    if (isDirective == 0)
                    {
                        return new DirectiveGraphBuilder(nodes, edges);
                    }

                    return new NonDirectiveGraphBuilder(nodes, edges);
            }
        }
    }

    private void getNodes()
    {
        Console.Clear();
        Console.WriteLine("Digite os ids dos nodes, digite \"e\" para sair do modo de adição de nós");

        while (true)
        {
            int? input = Utils._getNextIntInput("e", "Input inválido... este node será ignorado");
            if (input == null)
                break;
            nodes.Add(input.Value);
        }
    }

    private void getEdges()
    {
        while (true)
        {
            var nodesToString = nodes.Select(n => n.ToString()).ToList();
            string[] nodesWithExitOption = [.. nodesToString, "Sair"];
            int option1Selection = TuiTools.MenuSelect(new List<string>(nodesWithExitOption), "Selecione os nós para criar a aresta de origem");
            if (option1Selection == nodes.Count)
            {
                return;
            }
            int option1 = nodes[option1Selection];
            int option2Selection = TuiTools.MenuSelect(nodesToString, "Selecione o nó de destino");
            int option2 = nodes[option2Selection];
            edges.Add((option1, option2));
            Console.WriteLine($"Aresta criada: {option1} -> {option2}");
            TuiTools.WaitTillEnterPressed();
        }
    }

    private void printCurrentGraph()
    {
        Console.Clear();
        Console.WriteLine("Nós:");
        foreach (var node in nodes)
        {
            Console.WriteLine(node);
        }
        Console.WriteLine("Arestas:");
        foreach (var edge in edges)
        {
            Console.WriteLine($"{edge.Item1} -> {edge.Item2}");
        }
        TuiTools.WaitTillEnterPressed();
    }
}