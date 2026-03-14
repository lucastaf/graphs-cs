using garfos.Core;

namespace garfos.parser;

public class GraphExplorerParser
{
    private Graph _graph;

    public GraphExplorerParser(Graph graph)
    {
        _graph = graph;
    }

    public void ExplorerMenu()
    {
        while (true)
        {
            Console.WriteLine("Selecione uma opção");
            Console.WriteLine("e - Explorar o grafo a partir de um nó");
            Console.WriteLine("s - Pesquisar se um nó existe na matriz");
            Console.WriteLine("m - Exibir matriz de adjacência");
            Console.WriteLine("exit - Sair");
            string input = Console.ReadLine();
            switch (input)
            {
                case "exit":
                    return;
                case "e":
                    walkGraph();
                    break;
                case "s":
                    searchNode();
                    break;
                case "m":
                    printAdjacentMatrix();
                    break;
            }
        }
    }

    public void walkGraph()
    {
        Console.WriteLine($"Digite o nó de origem da consulta.");
        int? originId = Utils._getNextIntInput(null, "Nó inválido, tente novamente");

        Node originNode = _graph.GetNodeById(originId!.Value);
        Console.WriteLine("Selecione o modo de busca (p para busca em profundidade, l para busca em largura)");
        string input = Console.ReadLine();
        if (input == "p")
        {
            var order = _graph.DepthFirstSearch(originNode);
            Utils._printNodeList(order);
        }

        if (input == "l")
        {
            var order = _graph.BreadthFirstSearch(originNode);
            Utils._printNodeList(order);
        }
    }

    public void printAdjacentMatrix()
    {
        var adjacentMatrix = _graph.GetAdjacentMatrix();
        var nodes = _graph.GetNodes();
        Console.Write($"\t");
        foreach (var node in nodes)
        {
            Console.Write($"{node.Id}, ");
        }

        Console.Write("\n\n");
        int index = 0;
        foreach (var row in adjacentMatrix)
        {
            var currentNode = nodes.ElementAt(index);
            Console.Write($"{currentNode.Id}\t");
            foreach (var col in row)
            {
                Console.Write($"{col}, ");
            }

            Console.Write("\n");
            index++;
        }
    }

    public void searchNode()
    {
        Console.WriteLine("Digite o id do nó que deseja consulta");
        int nodeId = Utils._getNextIntInput(null, "Id inválido")!.Value;
        try
        {
            _graph.GetNodeById(nodeId);
            Console.WriteLine("Nó encontrado no grafo");
        }
        catch (Exception e)
        {
            Console.WriteLine("Nó não existente");
        }
    }
}