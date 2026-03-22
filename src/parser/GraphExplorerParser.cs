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
            var selection = TuiTools.MenuSelect(new List<string> {
                "Explorar o grafo a partir de um nó",
                "Pesquisar se um nó existe na matriz",
                "Exibir matriz de adjacência",
                "Sair" },
                "Selecione uma opção");
            switch (selection)
            {
                case 3:
                    return;
                case 0:
                    walkGraph();
                    break;
                case 1:
                    searchNode();
                    break;
                case 2:
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
        var selection = TuiTools.MenuSelect(new List<string>
        {
            "Busca em profundidade",
            "Busca em largura"
        }, "Selecione o modo de busca");
        if (selection == 0)
        {
            var order = _graph.DepthFirstSearch(originNode);
            Utils._printNodeList(order);
        }

        if (selection == 1)
        {
            var order = _graph.BreadthFirstSearch(originNode);
            Utils._printNodeList(order);
        }
        TuiTools.WaitTillEnterPressed();
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
        TuiTools.WaitTillEnterPressed();
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
        TuiTools.WaitTillEnterPressed();
    }
}