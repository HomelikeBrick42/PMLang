using System;

namespace PMCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("> ");
                var line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    return;

                if (line == "1 + 2 * 3")
                    Console.WriteLine("7");
                else
                    Console.WriteLine("ERROR: Invalid expression!");
            }
        }
    }
}
