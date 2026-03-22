using System;

public static class TuiTools
{

    public static int MenuSelect(List<string> options, string? title = null)
    {
        int selectedIndex = 0;
        while (true)
        {
            Console.Clear();
            if(title!= null)
            {
                Console.WriteLine(title);
                Console.WriteLine();
            }
            foreach (var option in options)
            {
                if (options.IndexOf(option) == selectedIndex)
                {
                    Console.WriteLine($"> {option}");
                }
                else
                {
                    Console.WriteLine($"  {option}");
                }
            }
            var pressedKey = Console.ReadKey();
            if (pressedKey.Key == ConsoleKey.Enter)
            {
                return selectedIndex;
            }
            if (pressedKey.Key == ConsoleKey.UpArrow)
            {
                selectedIndex = (selectedIndex - 1 + options.Count) % options.Count;
            }
            else if (pressedKey.Key == ConsoleKey.DownArrow)
            {
                selectedIndex = (selectedIndex + 1) % options.Count;
            }

        }
    }

    public static void WaitTillEnterPressed()
    {
        Console.WriteLine();
        Console.WriteLine("Pressione Enter para continuar...");
        while (Console.ReadKey().Key != ConsoleKey.Enter)
        {
            // Aguarda até que a tecla Enter seja pressionada
        }
    }
}
