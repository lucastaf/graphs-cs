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
            var selection = TuiTools.MenuSelect(
                [
                "Explorar o grafo a partir de um nó",
                "Pesquisar se um nó existe na matriz",
                "Exibir matriz de adjacência",
                "Visualizar fecho transitivo",
                "Sair" 
                ],
                "Selecione uma opção");
            switch (selection)
            {
                case 4:
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
                case 3:
                    transitiveClosure();
                    break;
            }
        }
    }

    public void walkGraph()
    {
        Node originNode = _selectNode("Selecione o nó de origem da consulta.");
        var selection = TuiTools.MenuSelect(
        [
            "Busca em profundidade",
            "Busca em largura"
        ], "Selecione o modo de busca");
        if (selection == 0)
        {
            var order = originNode.DepthFirstSearch(new (), null);
            Utils._printNodeList(order);
        }

        if (selection == 1)
        {
            var order = originNode.BreadthFirstSearch(new (), true);
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

    public void transitiveClosure()
    {
        Node originNode = _selectNode("Selecione o nó de origem da consulta.");

        int selection = TuiTools.MenuSelect(
        [
            "Fecho transitivo direto",
            "Fecho transitivo indireto"
        ], "Selecione o tipo de fecho transitivo");
        var directiveClosure = selection == 0 ? _graph.DirectTransitiveClosure(originNode) : _graph.InverseTransitiveClosure(originNode);
        foreach (var pair in directiveClosure)
        {
            Console.WriteLine($"Nó {pair.Item1.Id} a distância {pair.Item2}");
        }
        TuiTools.WaitTillEnterPressed();
    }

    private Node _selectNode(string title = "Selecione um nó")
    {
        string[] nodesList = _graph.GetNodes().Select(node => node.Id.ToString()).ToArray();
        int selection = TuiTools.MenuSelect(nodesList, title);
        return _graph.GetNodeById(int.Parse(nodesList[selection]));
    }
}