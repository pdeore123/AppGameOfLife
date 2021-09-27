using System;
using System.Threading;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            short size = 25;
            Helper helper = new Helper();
            Cell[,] grid = helper.ApplyGlider(helper.CreateGrid(size));

            for (int row = 0; row < grid.Length / size -1; row++)
            {
                Console.WriteLine();
                for (int col = 0; col < grid.Length / size -1; col++)
                {
                    if (grid[row, col].State)
                        Console.Write(" * ");
                    else if (!grid[row, col].State)
                        Console.Write(" . ");
                }
            }
            for (int i = 0; i < 20; i++)
            {
                Thread.Sleep(2000);
                grid = helper.Play(grid);
                Console.Clear();
                for (int row = 0; row < grid.Length / size - 1; row++)
                {
                    Console.WriteLine();
                    for (int col = 0; col < grid.Length / size - 1; col++)
                    {
                        if (grid[row, col].State)
                            Console.Write(" * ");
                        else if (!grid[row, col].State)
                            Console.Write(" . ");
                    }
                }
            }

            Console.ReadKey();

        }
    }
}
