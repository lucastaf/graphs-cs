// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
Grafo grafo = new Grafo(new List<int> { 1, 2, 3, 4, 5 }, new List<(int, int)> { (1, 2), (2, 4), (4, 5), (2, 3), (3, 1) });

// var matriz = grafo.GetMatrizDirigido();

// foreach (var row in matriz)
// {
//     foreach (var col in row)
//     {
//         Console.Write(col.ToString());
//     }
//     Console.Write("\n");
// }
// Console.WriteLine(matriz);

var navigationOrden = grafo.NavigateFrom(3);
foreach (var node in navigationOrden)
{
    Console.WriteLine(node + "\n");
}