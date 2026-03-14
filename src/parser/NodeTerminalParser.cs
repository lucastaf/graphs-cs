using garfos.Builder;
using garfos.Core;

namespace garfos.parser;

public class NodeTerminalParser
{
    public AbstractGraphBuilder readTerminal()
    {
        var nodes = getNodes();
        var edges = getEdges();
        bool isDirective = getIsDirectiveMode();
        if (isDirective)
        {
            return new DirectiveGraphBuilder(nodes, edges);
        }

        return new NonDirectiveGraphBuilder(nodes, edges);
    }

    public void walkGraph(Graph graph)
    {
        while (true)
        {
            Console.WriteLine($"Digite o nó de origem da consulta. Digite exit para sair");
            int? originId = _getNextIntInput("exit", "Nó inválido, tente novamente");
            if (originId == null)
                return;
            
            Node originNode = graph.GetNodeById(originId.Value);
            Console.WriteLine("Selecione o modo de busca (p para busca em profundidade, l para busca em largura)");
            string input = Console.ReadLine();
            if (input == "p")
            {
                var order = graph.DepthFirstSearch(originNode);
                _printNodeList(order);
            }

            if (input == "l")
            {
                var order = graph.BreadthFirstSearch(originNode);
                _printNodeList(order);
            }
            
        }
    }
    
    
    private List<int> getNodes()
    {
        List<int> nodes = new List<int>();
        Console.WriteLine("Digite os ids dos nodes, digite exit para continuar");

        while (true)
        {
            int? input = _getNextIntInput("exit", "Input inválido... este node será ignorado");
            if (input == null)
                break;
            nodes.Add(input.Value);
        }

        return nodes;
    }

    private List<(int, int)> getEdges()
    {
        List<(int, int)> edges = new List<(int, int)>();
        Console.WriteLine("Digite o nó de origem, seguido do destino, digite exit para sair");
        while (true)
        {
            int? input1 = _getNextIntInput("exit", "Input inválido... tente de novo");
            if (input1 == null)
                break;

            int? input2 = _getNextIntInput(null, "Input inválido... tente de novo");
            edges.Add(((int)input1, (int)input2!));
            Console.WriteLine($"conexão: {input1} -> {input2}");
        }

        return edges;
    }

    private bool getIsDirectiveMode()
    {
        Console.WriteLine("Selecione o modo de direção (d para diretivo, n para nao diretivo)");
        while (true)
        {
            string input = Console.ReadLine();
            if (input == "d")
                return true;
            if (input == "n")
                return false;
        }
    }

    private int? _getNextIntInput(string? exitCode, string errorMessage)
    {
        while (true)
        {
            try
            {
                string input = Console.ReadLine()!;
                if (exitCode != null && input == exitCode) return null;
                return int.Parse(input);
            }
            catch
            {
                Console.WriteLine(errorMessage);
            }
        }
    }

    private void _printNodeList(List<Node> list)
    {
        foreach (var item in list)
        {
            Console.WriteLine(item.Id);
        }
    }
}