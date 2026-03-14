namespace garfos.Core;

public static class Utils
{
    public static int? _getNextIntInput(string? exitCode, string errorMessage)
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

    public static void _printNodeList(List<(Node target, Node? origin)> list)
    {
        foreach (var item in list)
        {
            Console.WriteLine($"{item.origin?.Id.ToString() ?? "NULO"} -> {item.target.Id}");
        }
    }   
}