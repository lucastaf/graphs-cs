using System.Globalization;

namespace garfos.Core;

public class GraphSaver
{
    public void SaveGraph(string path, List<int> nodes, List<(int, int)> edges)
    {
        var lines = new List<string>
        {
            string.Join(",", nodes)
        };

        foreach (var edge in edges)
        {
            lines.Add($"{edge.Item1},{edge.Item2}");
        }

        var directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        File.WriteAllLines(path, lines);
    }

    public (List<int> Nodes, List<(int, int)> Edges) LoadGraph(string path)
    {
        var lines = File.ReadAllLines(path)
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .ToArray();

        var nodes = new List<int>();
        var edges = new List<(int, int)>();

        if (lines.Length == 0)
        {
            return (nodes, edges);
        }

        nodes = lines[0]
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(int.Parse)
            .ToList();

        for (int i = 1; i < lines.Length; i++)
        {
            var edgeParts = lines[i].Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (edgeParts.Length != 2)
            {
                continue;
            }

            edges.Add((int.Parse(edgeParts[0]), int.Parse(edgeParts[1])));
        }

        return (nodes, edges);
    }
}