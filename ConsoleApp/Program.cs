using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Begin program.");

            // Puzzle1.txt is medium difficulty
            // Puzzle2.txt is easy difficulty
            // Puzzle3.txt is hard difficulty
            Puzzle puzzle = new Puzzle("Puzzle1.txt");
            puzzle.Display();

            DateTime start = DateTime.Now;
            puzzle.Solve();
            TimeSpan elapsed = DateTime.Now.Subtract(start);

            Console.WriteLine("{0} solutions found in {1} seconds.", puzzle.Solutions.Count, elapsed.TotalSeconds);

            for (int i = 0; i < puzzle.Solutions.Count; i++)
            {
                Console.WriteLine("Solution {0}:", i + 1);
                int[,] solution = puzzle.Solutions[i];
                for (int row = 0; row < 9; row++)
                {
                    Console.WriteLine("{0}{1}{2}{3}{4}{5}{6}{7}{8}",
                        solution[row, 0],
                        solution[row, 1],
                        solution[row, 2],
                        solution[row, 3],
                        solution[row, 4],
                        solution[row, 5],
                        solution[row, 6],
                        solution[row, 7],
                        solution[row, 8]
                        );
                }
            }

            Console.WriteLine("End program.");
        }
    }
}
