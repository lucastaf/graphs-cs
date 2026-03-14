using garfos.Builder;
using garfos.Core;

namespace garfos.parser;

public class GraphCreatorParser
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
    
    private List<int> getNodes()
    {
        List<int> nodes = new List<int>();
        Console.WriteLine("Digite os ids dos nodes, digite exit para continuar");

        while (true)
        {
            int? input = Utils._getNextIntInput("exit", "Input inválido... este node será ignorado");
            if (input == null)
                break;
            nodes.Add(input.Value);
        }

        return nodes;
    }

    private List<(int, int)> getEdges()
    {
        List<(int, int)> edges = new List<(int, int)>();
        Console.WriteLine("Digite o nó de origem e o nó de desitno, seguindo o formato 'origem -> destino', digite 'exit' para continuar");
        while (true)
        {
            string input = Console.ReadLine();
            if (input == "exit")
                break;
            try
            {
                string[] parts = input.Split(" -> ");
                int input1 = int.Parse(parts[0]);
                int input2 =  int.Parse(parts[1]);
                edges.Add((input1, input2));
            }
            catch
            {
                Console.WriteLine("Input inválido... tente de novo");
            }
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
}